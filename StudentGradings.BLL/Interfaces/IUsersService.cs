using StudentGradings.BLL.Models;

namespace StudentGradings.BLL.Interfaces
{
    public interface IUsersService
    {
        Task<Guid> AddUserAsync(UserModel userModel);
        Task<string?> AuthenticateAsync(string email, string password);
        Task DeactivateUser(Guid id);
        Task DeleteUserAsync(Guid id);
        Task<List<UserModel>> GetAllUsersAsync();
        Task<UserModel> GetUserWithCoursesAndGradesAsync(Guid userId);
        Task UpdatePasswordByUserIdAsync(Guid id, string newPassword);
        Task UpdateUserAsync(Guid id, UserModel user);
    }
}