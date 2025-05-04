using StudentGradings.BLL.Models;
using StudentGradings.DAL.Models.Dtos;

namespace StudentGradings.BLL.Interfaces
{
    public interface IUserCoursesService
    {
        Task AddGradeByUserIdAndCourseIdAsync(Guid courseId, Guid userId, float grade);
        Task DeleteGradeByCourseIdAndUserIdAsync(Guid courseId, Guid userId);
        Task<List<UserCourseModel>> GetAllGradesByUserIdAsync(Guid userId);
        Task<List<UserCourseModel>> GetGradesByCourseIdAsync(Guid userId, Guid courseId);
        Task<UserCourseModel> GetUserCourseAsync(Guid courseId, Guid userId);
        Task UpdateGradeByCourseIdAndUserIdAsync(Guid courseId, Guid userId, float grade);
    }
}