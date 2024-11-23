namespace StudentGradings.DAL.Models.Dtos;

public class CourseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public TimeOnly Hours { get; set; }
    public StudentDto Student { get; set; }
    public IEnumerable<StudentDto> Students { get; set; }
}
