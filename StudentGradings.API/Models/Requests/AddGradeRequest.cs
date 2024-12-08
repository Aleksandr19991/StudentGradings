using StudentGradings.BLL.Models;

namespace StudentGradings.API.Models.Requests
{
    public class AddGradeRequest
    {
        public float Grade { get; set; }
        public UserModelBll User { get; set; }
        public CourseModelBll Course { get; set; }
    }
}
