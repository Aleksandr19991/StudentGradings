using StudentGradings.BLL.Models;

namespace StudentGradings.BLL.Interfaces
{
    public interface IUsersService
    {
        void AddUser(UserModelBll user);
        string? Authenticate(string email, string password);
        IEnumerable<UserModelBll> GetCoursesByUserId(Guid userId);
        UserModelBll GetUserRoleByUserId(Guid userId);
        void UpdateUser(UserModelBll user);
    }
}