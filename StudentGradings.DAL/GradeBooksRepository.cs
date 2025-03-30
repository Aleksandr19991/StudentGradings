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
        var existingGradeBook = await context.GradeBooks
            .FirstOrDefaultAsync(g => g.Course.Id == gradeBook.Course.Id && g.User.Id == gradeBook.User.Id);
        context.GradeBooks.Add(gradeBook);
        await context.SaveChangesAsync();
    }

    public async Task UpdateGradeByCourseIdAsync(GradeBookDto gradeBook, float grade)
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
        return await context.GradeBooks.Where(c => c.Course.Id == courseId).ToListAsync();
    }

    public async Task<List<GradeBookDto>> GetGradesByUserIdAsync(Guid userId)
    {
        return await context.GradeBooks.Where(c => c.User.Id == userId).ToListAsync();
    }
}