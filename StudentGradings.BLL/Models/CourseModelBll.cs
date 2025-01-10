namespace StudentGradings.BLL.Models;

public class CourseModelBll
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Hours { get; set; }
    public GradeBookModelBll? GradeBook { get; set; }
}
