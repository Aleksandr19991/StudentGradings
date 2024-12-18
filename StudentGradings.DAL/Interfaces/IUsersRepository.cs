using StudentGradings.CORE;
using StudentGradings.DAL.Models.Dtos;

namespace StudentGradings.DAL.Interfaces
{
    public interface IUsersRepository
    {
        Guid AddUser(UserDto user);
        void DeactivateUser(UserDto user);
        void DeleteUser(UserDto user);
        List<UserDto> GetAllUsers();
        List<CourseDto> GetCoursesByUserId(Guid userId);
        UserDto? GetUserByEmail(string email);
        UserDto? GetUserById(Guid id);
        void GetUserRoleByUserId(UserDto user, UserRole role);
        void UpdatePasswordByUserId(UserDto user, string password);
        void UpdateUser(UserDto user, UserDto ChangeUser);
    }
}