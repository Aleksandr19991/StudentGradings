namespace StudentGradings.BLL.Models;

public class CourseModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Hours { get; set; }
    public bool IsDeactevated { get; set; }
    public GradeBookModel? GradeBook { get; set; }
}
