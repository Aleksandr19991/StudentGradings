using StudentGradings.CORE;

namespace StudentGradings.BLL.Models;

public class UserModelWithoutPassword
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public bool IsDeactivated { get; set; }
    public UserRole Role { get; set; }
}
