using StudentGradings.BLL.Models;

namespace StudentGradings.BLL.Interfaces
{
    public interface IGradeBooksService
    {
        Guid AddGradeBook(GradeBookModelBll gradeBookId);
        void AddGradeByCourseId(Guid courseId, Guid userId);
        GradeBookModelBll GetGradeBook(Guid courseId, Guid userId);
        GradeBookModelBll GetGradesByCourseId(Guid courseId);
        void UpdateGradeByCourseId(Guid id, GradeBookModelBll gradeBook);
    }
}