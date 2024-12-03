using StudentGradings.DAL.Models.Dtos;

namespace StudentGradings.DAL.Interfaces
{
    public interface IUsersRepository
    {
        void AddUser(UserDto user);
        IEnumerable<CourseDto> GetCoursesByUserId(Guid userId);
        UserDto? GetUserByEmail(string email);
        UserDto GetUserRoleByUserId(Guid userId);
        void UpdateUser(UserDto user);
    }
}