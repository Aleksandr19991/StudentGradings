namespace StudentGradings.BLL.Models;

public class GradeBookModel
{
    public Guid UserId { get; set; }
    public Guid CourseId { get; set; }
    public float Grade { get; set; }
    public UserModel User { get; set; }
    public CourseModel Course { get; set; }
}
