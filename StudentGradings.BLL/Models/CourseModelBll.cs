namespace StudentGradings.BLL.Models;

public class CourseModelBll
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Hours { get; set; }
    public GradeBookModelBll? GradeBook { get; set; }
    public IEnumerable<UserModelBll>? Users { get; set; }
}
