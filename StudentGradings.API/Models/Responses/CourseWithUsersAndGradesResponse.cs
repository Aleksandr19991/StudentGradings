using StudentGradings.BLL.Models;

namespace StudentGradings.API.Models.Responses;

public class CourseWithUsersAndGradesResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Hours { get; set; }
    public GradeBookModel? GradeBook { get; set; }
    public List<UserResponse> Users { get; set; }
}
