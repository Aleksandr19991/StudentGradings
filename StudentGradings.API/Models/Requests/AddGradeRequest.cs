using StudentGradings.BLL.Models;

namespace StudentGradings.API.Models.Requests
{
    public class AddGradeRequest
    {
        public float Grade { get; set; }
        public UserRole Role { get; set; }
        public CourseModelBll Course { get; set; }
    }
}
