using StudentGradings.BLL.Models;

namespace StudentGradings.BLL.Interfaces
{
    public interface IGradeBooksService
    {
        Task<Guid> AddGradeBookAsync(GradeBookModel gradeBookModel);
        Task AddGradeByCourseIdAndUserIdAsync(Guid courseId, Guid userId);
        Task<GradeBookModel> GetGradeBookAsync(Guid courseId, Guid userId);
        Task<List<GradeBookModel>> GetGradesByCourseIdAsync(Guid courseId);
        Task<List<GradeBookModel>> GetAllGradesByCoursesAsync();
        Task UpdateGradeByCourseIdAndUserIdAsync(Guid courseId, Guid userId, GradeBookModel gradeBook);
    }
}