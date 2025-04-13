using StudentGradings.BLL.Models;

namespace StudentGradings.API.Models.Requests;

public class AddGradeRequest
{
    public float Grade { get; set; }
    public UserModel User { get; set; }
    public CourseModel Course { get; set; }
}