using StudentGradings.BLL.Models;

namespace StudentGradings.BLL.Interfaces
{
    public interface IGradeBooksService
    {
        Task<Guid> AddGradeBookAsync(GradeBookModelBll gradeBookId);
        Task AddGradeByCourseIdAsync(Guid courseId, Guid userId);
        Task<GradeBookModelBll> GetGradeBookAsync(Guid courseId, Guid userId);
        Task<GradeBookModelBll> GetGradesByCourseIdAsync(Guid courseId);
        Task UpdateGradeByCourseIdAsync(Guid id, GradeBookModelBll gradeBook);
    }
}