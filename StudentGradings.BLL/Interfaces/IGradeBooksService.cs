using StudentGradings.BLL.Models;

namespace StudentGradings.BLL.Interfaces
{
    public interface IGradeBooksService
    {
        Task<Guid> AddGradeBookAsync(GradeBookModel gradeBookModel);
        Task AddGradeByCourseIdAsync(Guid courseId, Guid userId);
        Task<GradeBookModel> GetGradeBookAsync(Guid courseId, Guid userId);
        Task<IEnumerable<GradeBookModel>> GetGradesByCourseIdAsync(Guid courseId);
        Task UpdateGradeByCourseIdAsync(Guid courseId, Guid userId, GradeBookModel gradeBook);
    }
}