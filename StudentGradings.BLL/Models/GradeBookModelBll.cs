namespace StudentGradings.BLL.Models;

public class GradeBookModelBll
{
    public Guid UserId { get; set; }
    public Guid CourseId { get; set; }
    public float Grade { get; set; }
    public UserModelBll User { get; set; }
    public CourseModelBll Course { get; set; }
}
