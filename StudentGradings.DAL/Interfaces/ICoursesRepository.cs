using StudentGradings.DAL.Models.Dtos;

namespace StudentGradings.DAL.Interfaces
{
    public interface ICoursesRepository
    {
        void AddCourse(CourseDto course);
        void AddGradeByCourseId(GradeBookDto gradeBook, Guid courseId);
        IEnumerable<CourseDto> GetGradesByCourseId(Guid courseId);
        IEnumerable<UserDto> GetUsersByCourseId(Guid courseId);
        void UpdateCourse(CourseDto course);
        void UpdateGradeByCourseId(GradeBookDto gradeBook, Guid courseId);
    }
}