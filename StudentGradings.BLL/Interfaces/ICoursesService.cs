using StudentGradings.BLL.Models;

namespace StudentGradings.BLL.Interfaces
{
    public interface ICoursesService
    {
        Task<Guid> AddCourseAsync(CourseModelBll courseId);
        Task DeactivateCourseAsync(Guid id);
        Task DeleteCourseAsync(Guid id);
        Task<List<CourseModelBll>> GetAllCoursesAsync();
        Task<CourseModelBll> GetCourseByIdAsync(Guid id);
        Task<CourseModelBll> GetCourseWithUsersAndGradesAsync(Guid courseId);
        Task UpdateCourseAsync(Guid id, CourseModelBll newCourseId);
    }
}