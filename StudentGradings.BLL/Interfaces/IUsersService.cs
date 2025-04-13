using StudentGradings.BLL.Models;

namespace StudentGradings.BLL.Interfaces
{
    public interface IUsersService
    {
        Task<string?> AuthenticateAsync(string email, string password);
        Task<Guid> AddUserAsync(UserModel userModel);
        Task UpdateUserAsync(Guid id, UserModel user);
        Task UpdatePasswordAsync(Guid id, string newPassword);
        Task<UserModel> GetUserByIdAsync(Guid id);
        Task<List<UserModel>> GetAllUsersAsync();
        Task<UserModel> GetUserWithCoursesAndGradesAsync(Guid userId);
        Task DeactivateUser(Guid id);
        Task DeleteUserAsync(Guid id);
    }
}