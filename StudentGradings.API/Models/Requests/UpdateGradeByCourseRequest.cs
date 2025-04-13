using StudentGradings.API.Models.Responses;

namespace StudentGradings.API.Models.Requests;

public class UpdateGradeByCourseRequest
{
    public CourseResponse Course { get; set; }
    public UserResponse User { get; set; }
    public float Grade { get; set; }
}