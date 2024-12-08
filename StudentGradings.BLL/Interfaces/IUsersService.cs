using StudentGradings.BLL.Models;

namespace StudentGradings.BLL.Interfaces
{
    public interface IUsersService
    {
        Guid AddUser(UserModelBll userId);
        string? Authenticate(string email, string password);
        void DeactivateUser(Guid id);
        void DeleteUser(Guid id);
        List<UserModelBll> GetAllUsers();
        UserModelBll GetCoursesByUserId(Guid userId);
        UserRoleModelBll GetUserRoleByUserId(Guid id, UserRoleModelBll role);
        void UpdatePasswordByUserId(Guid id, UserModelBll user);
        void UpdateUser(Guid id, UserModelBll newUser);
    }
}