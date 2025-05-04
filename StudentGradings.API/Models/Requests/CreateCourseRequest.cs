namespace StudentGradings.API.Models.Requests;

public class CreateCourseRequest
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Hours { get; set; }
    public string Semester { get; set; }
}