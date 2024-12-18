using StudentGradings.CORE;

namespace StudentGradings.API.Models.Responses;

public class UserResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public UserRole Role { get; set; }
}
