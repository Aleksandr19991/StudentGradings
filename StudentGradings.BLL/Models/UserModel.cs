using StudentGradings.CORE;

namespace StudentGradings.BLL.Models;

public class UserModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public bool IsDeactevated { get; set; }
    public UserRole Role { get; set; }
    public GradeBookModel? GradeBook { get; set; }
}
