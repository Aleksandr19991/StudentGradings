using StudentGradings.BLL.Models;

namespace StudentGradings.BLL.Interfaces
{
    public interface ICoursesService
    {
        void AddGradeByCourseId(GradeBookModelBll gradeBook, Guid courseId);
        GradeBookModelBll GetGradesByCourseId(Guid courseId);
        UserModelBll GetUsersByCourseId(Guid courseId);
        void UpdateGradeByCourseId(GradeBookModelBll gradeBook, Guid courseId);
    }
}