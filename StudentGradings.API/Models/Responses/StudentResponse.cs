namespace StudentGradings.API.Models.Responses;

public class StudentResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public float Grade { get; set; }
}
