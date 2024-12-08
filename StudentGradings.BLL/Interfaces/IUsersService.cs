using StudentGradings.BLL.Models;

namespace StudentGradings.BLL.Interfaces
{
    public interface IUsersService
    {
        void AddUser(UserModelBll userId);
        string? Authenticate(string email, string password);
        void DeactivateUser(Guid id);
        void DeleteCourse(Guid id);
        List<UserModelBll> GetAllUsers();
        UserModelBll GetCoursesByUserId(Guid userId);
        UserRoleModelBll GetUserRoleByUserId(Guid id, UserRoleModelBll role);
        void UpdatePasswordByUserId(Guid id, UserModelBll user);
        void UpdateUser(Guid id, UserModelBll newUser);
    }
}