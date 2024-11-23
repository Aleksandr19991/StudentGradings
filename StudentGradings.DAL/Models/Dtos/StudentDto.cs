namespace StudentGradings.DAL.Models.Dtos;

public class StudentDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public float Grade { get; set; }
    public CourseDto Course { get; set; }
}
