using StudentGradings.BLL.Models;

namespace StudentGradings.BLL.Interfaces
{
    public interface IGradeBooksService
    {
        Task AddGradeByCourseIdAndUserIdAsync(Guid courseId, Guid userId, float grade);
        Task<GradeBookModel> GetGradeBookAsync(Guid courseId, Guid userId);
        Task<List<GradeBookModel>> GetGradesByCourseIdAsync(Guid courseId);
        Task<List<GradeBookModel>> GetAllGradesByCoursesAsync();
        Task UpdateGradeByCourseIdAndUserIdAsync(Guid courseId, Guid userId, float grade);
    }
}