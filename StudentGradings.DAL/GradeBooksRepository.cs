using Microsoft.EntityFrameworkCore;
using StudentGradings.DAL.Interfaces;
using StudentGradings.DAL.Models.Dtos;

namespace StudentGradings.DAL;

public class GradeBooksRepository(StudentGradingsContext context) : IGradeBooksRepository
{
    public async Task<Guid> AddGradeBookAsync(GradeBookDto gradeBook)
    {
        context.GradeBooks.Add(gradeBook);
        await context.SaveChangesAsync();
        return gradeBook.Id;
    }

    public async Task AddGradeByCourseIdAndUserIdAsync(GradeBookDto gradeBook)
    {
        var existingGradeBook = await context.GradeBooks
            .FirstOrDefaultAsync(g => g.Course.Id == gradeBook.Course.Id && g.User.Id == gradeBook.User.Id);
        if (existingGradeBook != null)
        {
            existingGradeBook.Grade = gradeBook.Grade;
        }
        else
        {
            var newGradeBook = new GradeBookDto
            {
                Course = gradeBook.Course,
                User = gradeBook.User,
                Grade = gradeBook.Grade
            };
            context.GradeBooks.Add(newGradeBook);
        }
        await context.SaveChangesAsync();
    }

    public async Task UpdateGradeByCourseIdAndUserIdAsync(GradeBookDto gradeBook, float grade)
    {
        var existingGradeBook = await context.GradeBooks
           .FirstOrDefaultAsync(g => g.Course.Id == gradeBook.Course.Id && g.User.Id == gradeBook.User.Id);
        existingGradeBook.Grade = grade;
        await context.SaveChangesAsync();
    }

    public async Task<GradeBookDto?> GetGradeBookAsync(Guid courseId, Guid userId)
    {
        return await context.GradeBooks.FirstOrDefaultAsync(c => c.Course.Id == courseId && c.User.Id == userId);
    }

    public async Task<List<GradeBookDto>> GetGradesByCourseIdAsync(Guid courseId)
    {
        var gradesCourse = await context.GradeBooks
           .Where(c => c.Course.Id == courseId)
           .Select(g => new GradeBookDto
           {
               Id = g.Id,
               CourseId = g.Course.Id,
               Grade = g.Grade
           }).ToListAsync();

        return gradesCourse;
    }

    public async Task<List<GradeBookDto>> GetAllGradesWithCoursesAsync()
    {
        var allGrades = await context.GradeBooks
            .Include(g => g.User)
            .Include(g => g.Course)
            .Select(g => new GradeBookDto
            {
                Id = g.Id,
                UserId = g.UserId,
                CourseId = g.CourseId,
                Grade = g.Grade,
                User = new UserDto
                {
                    Id = g.User.Id,
                    Name = g.User.Name
                },
                Course = new CourseDto
                {
                    Id = g.Course.Id,
                    Name = g.Course.Name
                }
            })
            .ToListAsync();

        return allGrades;
    }

    public async Task<bool> GradeExistsByCourseIdAndUserIdAsync(Guid courseId, Guid userId)
    {
        return await context.GradeBooks.AnyAsync(g => g.CourseId == courseId && g.UserId == userId);
    }

    public async Task DeleteGradeByCourseIdAndUserIdAsync(Guid courseId, Guid userId)
    {
        var gradeToRemove = await context.GradeBooks.FirstOrDefaultAsync(g => g.CourseId == courseId && g.UserId == userId);
        context.GradeBooks.Remove(gradeToRemove);
        await context.SaveChangesAsync();
    }
}