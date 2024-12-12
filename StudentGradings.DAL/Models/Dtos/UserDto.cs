namespace StudentGradings.DAL.Models.Dtos;

public class UserDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public bool IsDeactevated { get; set; }
    public UserRoleDto? Role { get; set; }
    public IEnumerable<CourseDto> Courses { get; set; } = [];
}
