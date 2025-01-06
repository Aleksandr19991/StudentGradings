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

    public async Task AddGradeByCourseIdAsync(GradeBookDto gradeBook)
    {
        context.GradeBooks.Add(gradeBook);
        await context.SaveChangesAsync();
    }

    public async Task UpdateGradeByCourseIdAsync(GradeBookDto gradeBook, float grade)
    {
        gradeBook.Grade = grade;
        await context.SaveChangesAsync();
    }

    public async Task<GradeBookDto?> GetGradeBookAsync(Guid courseId, Guid userId)
    {
        var gradeBook = context.GradeBooks.Where(c => c.Course.Id == courseId && c.User.Id == userId).SingleOrDefaultAsync();
        return await gradeBook;
    }

    public async Task<List<GradeBookDto>> GetGradesByCourseIdAsync(Guid courseId)
    {
        var gradesCourse = context.GradeBooks.Where(c => c.Id == courseId).ToListAsync();
        return await gradesCourse;
    }

    public async Task<List<GradeBookDto>> GetGradesByUserIdAsync(Guid userId)
    {
        var gradesUser = context.GradeBooks.Where(c => c.Id == userId).ToListAsync();
        return await gradesUser;
    }
}
