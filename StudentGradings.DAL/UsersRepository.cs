using Microsoft.EntityFrameworkCore;
using StudentGradings.DAL.Interfaces;
using StudentGradings.DAL.Models.Dtos;

namespace StudentGradings.DAL;

public class UsersRepository(StudentGradingsContext context) : IUsersRepository
{
    public Guid AddUser(UserDto user)
    {
        context.Users.Add(user);
        context.SaveChanges();
        return user.Id;
    }

    public void UpdateUser(UserDto user, UserDto changeUser)
    {
        user.Name = changeUser.Name;
        user.LastName = changeUser.LastName;
        user.Phone = changeUser.Phone;
        user.Email = changeUser.Email;
        context.SaveChanges();
    }

    public void UpdatePasswordByUserId(UserDto user, string password)
    {
        user.Password = password;
        context.SaveChanges();
    }

    public UserDto? GetUserById(Guid id) => context.Users.SingleOrDefault(c => c.Id == id);

    public UserDto? GetUserByEmail(string email) => context.Users.SingleOrDefault(c => c.Email == email);

    public void GetUserRoleByUserId(UserDto user, UserRoleDto role)
    {
        user.Role = role;
        context.SaveChanges();
    }

    public List<CourseDto> GetCoursesByUserId(Guid userId)
    {
        var users = context.Users.Where(c => c.Id == userId).Include(c => c.Courses).FirstOrDefault();
        var courses = users.Courses.ToList();
        return courses;
    }

    public void DeactivateUser(UserDto user)
    {
        user.IsDeactevated = true;
        context.SaveChanges();
    }

    public void DeleteUser(UserDto user)
    {
        context.Users.Remove(user);
        context.SaveChanges();
    }
}
