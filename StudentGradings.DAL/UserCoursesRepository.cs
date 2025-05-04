using Microsoft.EntityFrameworkCore;
using StudentGradings.DAL.Interfaces;
using StudentGradings.DAL.Models.Dtos;

namespace StudentGradings.DAL;

public class UserCoursesRepository(StudentGradingsContext context) : IUserCoursesRepository
{

    public async Task AddGradeByUserIdAndCourseIdAsync(Guid userId, Guid courseId, float grade)
    {
        var newUserCourse = new UserCourseDto
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            CourseId = courseId,
            Grade = grade,
            DateAssigned = DateTime.UtcNow
        };
        context.UserCourses.Add(newUserCourse);
        await context.SaveChangesAsync();
    }

    public async Task UpdateGradeByCourseIdAndUserIdAsync(Guid userId, Guid courseId, float newGrade)
    {
        var existingUserCourse = await context.UserCourses
           .FirstOrDefaultAsync(uc => uc.UserId == userId && uc.CourseId == courseId);
        existingUserCourse.Grade = newGrade;
        existingUserCourse.DateAssigned = DateTime.UtcNow;
        await context.SaveChangesAsync();
    }

    public async Task<UserCourseDto?> GetUserCourseAsync(Guid courseId, Guid userId)
    {
        return await context.UserCourses.FirstOrDefaultAsync(c => c.Course.Id == courseId && c.User.Id == userId);
    }

    public async Task<List<UserCourseDto>> GetGradesByCourseIdAsync(Guid userId, Guid courseId)
    {
        var gradesCourse = await context.UserCourses
           .Where(c => c.User.Id == userId && c.Course.Id == courseId)
           .Select(g => new UserCourseDto
           {
               UserId = g.User.Id,
               CourseId = g.Course.Id,
               Grade = g.Grade,
               DateAssigned = g.DateAssigned
           }).ToListAsync();

        return gradesCourse;
    }

    public async Task<List<UserCourseDto>> GetAllGradesByUserIdAsync(Guid userId)
    {
        var allGrades = await context.UserCourses
            .Include(g => g.User)
            .Include(g => g.Course)
            .Where(g=> g.UserId == userId)
            .Select(g => new UserCourseDto
            {
                Id = g.Id,
                UserId = g.UserId,
                CourseId = g.CourseId,
                Grade = g.Grade,
                DateAssigned = g.DateAssigned,
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

    public async Task<bool> GradeExistsByCourseIdAndUserIdAsync(Guid userId, Guid courseId)
    {
        return await context.UserCourses.AnyAsync(g => g.UserId == userId && g.CourseId == courseId);
    }

    public async Task DeleteGradeByCourseIdAndUserIdAsync(Guid userId, Guid courseId)
    {
        var gradeToRemove = await context.UserCourses.FirstOrDefaultAsync(g => g.UserId == userId && g.CourseId == courseId);
        context.UserCourses.Remove(gradeToRemove);
        await context.SaveChangesAsync();
    }
}