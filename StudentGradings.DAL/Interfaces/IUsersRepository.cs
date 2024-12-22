using StudentGradings.CORE;
using StudentGradings.DAL.Models.Dtos;

namespace StudentGradings.DAL.Interfaces
{
    public interface IUsersRepository
    {
        Task<Guid> AddUserAsync(UserDto user);
        Task DeactivateUserAsync(UserDto user);
        Task DeleteUserAsync(UserDto user);
        Task<List<UserDto>> GetAllUsersAsync();
        Task<UserDto?> GetUserByEmailAsync(string email);
        Task<UserDto?> GetUserByIdAsync(Guid id);
        Task GetUserRoleByUserIdAsync(UserDto user, UserRole role);
        Task<UserDto?> GetUserWithCoursesAndGradesAsync(Guid id);
        Task UpdatePasswordByUserIdAsync(UserDto user, string password);
        Task UpdateUserAsync(UserDto user, UserDto changeUser);
    }
}