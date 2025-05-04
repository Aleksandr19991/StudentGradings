namespace StudentGradings.BLL.Models;

public class CourseModelShort
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Hours { get; set; }
    public string Semester { get; set; }
    public bool IsDeactivated { get; set; }
}