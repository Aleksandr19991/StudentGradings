namespace StudentGradings.API.Models.Responses;

public class CourseResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public float Grade { get; set; }
    public TimeOnly Hours { get; set; }
    public List<StudentResponse> Students { get; set; }
}
