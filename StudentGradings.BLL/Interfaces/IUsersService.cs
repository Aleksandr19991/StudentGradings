using StudentGradings.BLL.Models;

namespace StudentGradings.BLL.Interfaces
{
    public interface IUsersService
    {
        Task<Guid> AddUserAsync(UserModelBll userId);
        Task<string?> AuthenticateAsync(string email, string password);
        Task DeactivateUser(Guid id);
        Task DeleteUserAsync(Guid id);
        Task<List<UserModelBll>> GetAllUsersAsync();
        Task<UserModelBll> GetUserWithCoursesAndGradesAsync(Guid userId);
        Task UpdatePasswordByUserIdAsync(Guid id, UserModelBll user);
        Task UpdateUserAsync(Guid id, UserModelBll newUser);
    }
}