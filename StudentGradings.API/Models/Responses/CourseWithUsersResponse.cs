namespace StudentGradings.API.Models.Responses;

public class CourseWithUsersResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public List<UserResponse> Users { get; set; }
}
