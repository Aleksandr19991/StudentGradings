namespace StudentGradings.API.Models.Responses;

public class GradeBookResponse
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid CourseId { get; set; }
    public float Grade { get; set; }
}