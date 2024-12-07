using StudentGradings.DAL.Models.Dtos;

namespace StudentGradings.DAL.Interfaces
{
    public interface ICoursesRepository
    {
        Guid AddCourse(CourseDto course);
        void DeactivateUser(CourseDto course);
        void DeleteCourse(CourseDto course);
        CourseDto? GetCourseById(Guid id);
        List<UserDto> GetUsersByCourseId(Guid courseId);
        void UpdateCourse(CourseDto course, CourseDto changeCourse);
    }
}