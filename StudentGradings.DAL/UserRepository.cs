using Microsoft.EntityFrameworkCore;
using StudentGradings.DAL.Models.Dtos;

namespace StudentGradings.DAL;

public class UserRepository
{
    private Context _context;

    public UserRepository()
    {
        _context = new Context();
    }
   
    public void AddUser(UserDto user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();
    }

    public void UpdateUser(UserDto user) 
    {
        _context.Users.Update(user);
        _context.SaveChanges();
    }

    public UserDto? GetUserByEmail(string email)
    {
        var user = _context.Users.Where(u => u.Email == email).SingleOrDefault();
        return user;
    }

    public UserDto GetUserRoleByUserId(Guid userId) 
    {
        var role = _context.Users.Include(u => u.Role).Where(r => r.Id == userId).FirstOrDefault();
        return role;
    }

    public IEnumerable<CourseDto> GetCoursesByUserId(Guid userId)
    {
        var courses = _context.Users.Include(u => u.Courses).Where(c => c.Id == userId).FirstOrDefault();
        return _context.Courses.ToList();
    }
}
