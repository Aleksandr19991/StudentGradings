namespace StudentGradings.BLL.Models;

public class CourseModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Hours { get; set; }
    public string Semester { get; set; }
    public bool IsDeactivated { get; set; }
    public ICollection<UserCourseModel> UserCourses { get; set; } = new List<UserCourseModel>();
}