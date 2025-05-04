namespace StudentGradings.API.Models.Requests;

public class CreateGradeRequest
{
    public Guid UserId { get; set; }
    public Guid CourseId { get; set; }
    public float Grade { get; set; }
    public DateTime DateAssigned { get; set; }
}