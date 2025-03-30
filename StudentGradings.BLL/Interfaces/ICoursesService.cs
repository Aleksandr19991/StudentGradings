using StudentGradings.BLL.Models;

namespace StudentGradings.BLL.Interfaces
{
    public interface ICoursesService
    {
        Task<Guid> AddCourseAsync(CourseModel courseId);
        Task DeactivateCourseAsync(Guid id);
        Task DeleteCourseAsync(Guid id);
        Task<List<CourseModel>> GetAllCoursesAsync();
        Task<CourseModel> GetCourseByIdAsync(Guid id);
        Task<CourseModel> GetCourseWithUsersAndGradesAsync(Guid courseId);
        Task UpdateCourseAsync(Guid id, CourseModel newCourseId);
    }
}