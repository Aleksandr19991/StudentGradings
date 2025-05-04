using StudentGradings.BLL.Models;

namespace StudentGradings.API.Models.Responses;

public class CourseWithGradeResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Hours { get; set; }
    public string Semester { get; set; }
    public float Grade { get; set; }
    public DateTime DateAssigned { get; set; }
}