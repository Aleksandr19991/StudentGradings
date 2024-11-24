namespace StudentGradings.API.Models.Responses;

public class GradeBookResponse
{
    public Guid Id { get; set; }
    public CourseResponse Course { get; set; }
    public UserResponse User { get; set; }
    public float Grade { get; set; }
}
