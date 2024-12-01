namespace StudentGradings.BLL.Models;

public class UserModelBll
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string LastName { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    IEnumerable<CourseModelBll> courses { get; set; }
}
