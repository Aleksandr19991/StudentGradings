using Microsoft.EntityFrameworkCore;
using StudentGradings.DAL.Interfaces;
using StudentGradings.DAL.Models.Dtos;

namespace StudentGradings.DAL;

public class CoursesRepository(StudentGradingsContext context) : ICoursesRepository
{
    public async Task<Guid> AddCourseAsync(CourseDto course)
    {
        context.Courses.Add(course);
        await context.SaveChangesAsync();
        return course.Id;
    }

    public async Task UpdateCourseAsync(CourseDto course, CourseDto changeCourse)
    {
        course.Name = changeCourse.Name;
        course.Description = changeCourse.Description;
        course.Hours = changeCourse.Hours;
        await context.SaveChangesAsync();
    }

    public async Task<CourseDto?> GetCourseByIdAsync(Guid id) => await context.Courses.SingleOrDefaultAsync(c => c.Id == id);

    public async Task<List<CourseDto>> GetAllCoursesAsync()
    {
        return await context.Courses.Where(c => c.IsDeactivated == false).ToListAsync();
    }

    public async Task<CourseDto?> GetCourseWithUsersAndGradesAsync(Guid id) =>
        await context.Courses
        .Include(c => c.GradeBooks)
        .ThenInclude(c => c.User)
        .SingleOrDefaultAsync(c => c.Id == id);

    public async Task DeactivateCourseAsync(CourseDto course)
    {
        course.IsDeactivated = true;
        await context.SaveChangesAsync();
    }

    public async Task DeleteCourseAsync(CourseDto course)
    {
        context.Courses.Remove(course);
        await context.SaveChangesAsync();
    }
}