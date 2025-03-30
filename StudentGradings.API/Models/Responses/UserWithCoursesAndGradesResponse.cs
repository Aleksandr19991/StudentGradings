using StudentGradings.BLL.Models;
using StudentGradings.CORE;

namespace StudentGradings.API.Models.Responses;

public class UserWithCoursesAndGradesResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public UserRole Role { get; set; }
    public GradeBookModel? GradeBook { get; set; }
    public List<CourseResponse>? Courses { get; set; }
}
