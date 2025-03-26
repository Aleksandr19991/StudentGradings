namespace StudentGradings.API.Models.Requests;

public class CreateGradeBookRequest
{
    public Guid UserId { get; set; }
    public Guid CourseId { get; set; }
    public float Grade { get; set; }
}
