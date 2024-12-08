using StudentGradings.DAL.Models.Dtos;

namespace StudentGradings.DAL.Interfaces
{
    public interface ICoursesRepository
    {
        Guid AddCourse(CourseDto course);
        void DeactivateCourse(CourseDto course);
        void DeleteCourse(CourseDto course);
        List<CourseDto> GetAllCourses();
        CourseDto? GetCourseById(Guid id);
        List<UserDto> GetUsersByCourseId(Guid courseId);
        void UpdateCourse(CourseDto course, CourseDto changeCourse);
    }
}