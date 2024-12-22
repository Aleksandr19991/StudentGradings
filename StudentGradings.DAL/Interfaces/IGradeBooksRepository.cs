using StudentGradings.DAL.Models.Dtos;

namespace StudentGradings.DAL.Interfaces
{
    public interface IGradeBooksRepository
    {
        Task<Guid> AddGradeBookAsync(GradeBookDto gradeBook);
        Task AddGradeByCourseIdAsync(GradeBookDto gradeBook);
        Task<GradeBookDto?> GetGradeBookAsync(Guid courseId, Guid userId);
        Task<List<GradeBookDto>> GetGradesByCourseIdAsync(Guid courseId);
        Task<List<GradeBookDto>> GetGradesByUserIdAsync(Guid userId);
        Task UpdateGradeAsync(GradeBookDto gradeBook, float grade);
    }
}