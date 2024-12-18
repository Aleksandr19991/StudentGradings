using StudentGradings.CORE;

namespace StudentGradings.API.Models.Requests;

public class RegisterUserRequest
{
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public UserRole Role { get; set; }
}
