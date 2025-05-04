namespace StudentGradings.BLL.Models;

public class UserCourseModel
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid CourseId { get; set; }
    public float Grade { get; set; }
    public DateTime DateAssigned { get; set; }
    public UserModel User { get; set; }
    public CourseModel Course { get; set; }
}
