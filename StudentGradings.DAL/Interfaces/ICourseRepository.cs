using StudentGradings.DAL.Models.Dtos;

namespace StudentGradings.DAL.Interfaces;

public interface ICourseRepository
{
    void AddCourse(CourseDto course);
    void AddGradeByCourseId(Guid courseId);
    GradeBookDto GetGradeByCourseId(Guid courseId);
    IEnumerable<CourseDto> GetGradesByAllCourses(Guid courseId);
    IEnumerable<UserDto> GetUsersByCourseId(Guid courseId);
    void UpdateCourse(CourseDto course);
    void UpdateGradeByCourseId(Guid courseId);
}