using StudentGradings.BLL.Models;

namespace StudentGradings.BLL.Interfaces
{
    public interface ICoursesService
    {
        Guid AddCourse(CourseModelBll courseId);
        void DeactivateCourse(Guid id);
        void DeleteCourse(Guid id);
        List<CourseModelBll> GetAllCourses();
        CourseModelBll GetCourseById(Guid id);
        UserModelBll GetUsersByCourseId(Guid courseId);
        void UpdateCourse(Guid id, CourseModelBll newCourseId);
    }
}