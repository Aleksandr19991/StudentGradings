using StudentGradings.DAL.Models.Dtos;

namespace StudentGradings.DAL.Interfaces
{
    public interface ICoursesRepository
    {
        Task<Guid> AddCourseAsync(CourseDto course);
        Task DeactivateCourseAsync(CourseDto course);
        Task DeleteCourseAsync(CourseDto course);
        Task<List<CourseDto>> GetAllCoursesAsync();
        Task<CourseDto?> GetCourseByIdAsync(Guid id);
        Task<CourseDto?> GetCourseWithUsersAndGradesAsync(Guid id);
        Task UpdateCourseAsync(CourseDto course, CourseDto changeCourse);
    }
}