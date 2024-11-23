namespace StudentGradings.API.Models.Responses
{
    public class CourseWithStudents
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<StudentResponse> Students { get; set; }
    }
}
