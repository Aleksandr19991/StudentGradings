namespace StudentGradings.DAL.Models.Dtos;

public class CourseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Hours { get; set; }
    public bool IsDeactivated { get; set; }
    public GradeBookDto? GradeBook { get; set; }
    public IEnumerable<UserDto>? Users { get; set; }
}
