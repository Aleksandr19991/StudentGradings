using StudentGradings.API.Models.Responses;

namespace StudentGradings.API.Models.Requests;

public class UpdateGradeRequest
{
    public float Grade { get; set; }
}