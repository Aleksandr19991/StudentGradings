using StudentGradings.BLL.Models;

namespace StudentGradings.BLL.Interfaces
{
    public interface ICoursesService
    {
        Guid AddCourse(CourseModelBll courseId);
        Guid AddGradeBook(GradeBookModelBll gradeBookId);
        void AddGradeByCourseId(Guid courseId, Guid userId);
        void DeactivateCourse(Guid id);
        void DeleteCourse(Guid id);
        List<CourseModelBll> GetAllCourses();
        CourseModelBll GetCourseById(Guid id);
        GradeBookModelBll GetGradesByCourseId(Guid courseId);
        UserModelBll GetUsersByCourseId(Guid courseId);
        void UpdateCourse(Guid id, CourseModelBll newCourseId);
        void UpdateGradeByCourseId(GradeBookModelBll gradeBook);
    }
}