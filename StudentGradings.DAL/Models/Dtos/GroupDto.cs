namespace StudentGradings.DAL.Models.Dtos;

public class GroupDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public UserDto User { get; set; }
    public CourseDto Course { get; set; }
    public GradeBookDto GradeBook { get; set; }
}
