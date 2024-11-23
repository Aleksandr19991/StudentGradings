using Microsoft.EntityFrameworkCore;
using StudentGradings.DAL.Models.Dtos;

namespace StudentGradings.DAL;

public class UserRepository
{
    Context context = new();

    public void AddUser(UserDto user)
    {
        context.Users.Add(user);
        context.SaveChanges();
    }

    public void UpdateUser(UserDto user) 
    {
        context.Users.Update(user);
        context.SaveChanges();
    }

    public UserDto GetUserRoleByUserId(Guid userId) 
    {
        var role = context.Users.Include(u => u.Role).Where(r => r.Id == userId).FirstOrDefault();
        return role;
    }

    public IEnumerable<CourseDto> GetCoursesByUserId(Guid userId)
    {
        var courses = context.Users.Include(u => u.Courses).Where(c => c.Id == userId).FirstOrDefault();
        return context.Courses.ToList();
    }
}
