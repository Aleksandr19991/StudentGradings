using StudentGradings.DAL.Models.Dtos;

namespace StudentGradings.DAL.Interfaces
{
    public interface IUserCoursesRepository
    {
        Task AddGradeByUserIdAndCourseIdAsync(Guid userId, Guid courseId, float grade);
        Task DeleteGradeByCourseIdAndUserIdAsync(Guid courseId, Guid userId);
        Task<List<UserCourseDto>> GetAllGradesByUserIdAsync(Guid userId);
        Task<List<UserCourseDto>> GetGradesByCourseIdAsync(Guid userId, Guid courseId);
        Task<UserCourseDto?> GetUserCourseAsync(Guid courseId, Guid userId);
        Task<bool> GradeExistsByCourseIdAndUserIdAsync(Guid courseId, Guid userId);
        Task UpdateGradeByCourseIdAndUserIdAsync(Guid userId, Guid courseId, float newGrade);
    }
}