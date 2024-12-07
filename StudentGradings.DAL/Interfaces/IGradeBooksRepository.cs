using StudentGradings.DAL.Models.Dtos;

namespace StudentGradings.DAL.Interfaces
{
    public interface IGradeBooksRepository
    {
        Guid AddGradeBook(GradeBookDto gradeBook);
        void AddGradeByCourseId(GradeBookDto gradeBook);
        void DeactivateGradeBook(GradeBookDto gradeBook);
        GradeBookDto? GetGradeBook(Guid courseId, Guid userId);
        List<GradeBookDto> GetGradesByCourseId(Guid courseId);
        List<GradeBookDto> GetGradesByUserId(Guid userId);
        void UpdateGrade(GradeBookDto gradeBook, float grade);
    }
}