using StudentGradings.DAL.Interfaces;
using StudentGradings.DAL.Models.Dtos;

namespace StudentGradings.DAL;

public class GradeBooksRepository(StudentGradingsContext context) : IGradeBooksRepository
{
    public Guid AddGradeBook(GradeBookDto gradeBook)
    {
        context.GradeBooks.Add(gradeBook);
        context.SaveChanges();
        return gradeBook.Id;
    }

    public void AddGradeByCourseId(GradeBookDto gradeBook)
    {
        context.GradeBooks.Add(gradeBook);
        context.SaveChanges();
    }

    public void UpdateGrade(GradeBookDto gradeBook, float grade)
    {
        gradeBook.Grade = grade;
        context.SaveChanges();
    }

    public GradeBookDto? GetGradeBook(Guid courseId, Guid userId)
    {
        var gradeBook = context.GradeBooks.Where(c => c.Course.Id == courseId && c.User.Id == userId).SingleOrDefault();
        return gradeBook;
    }

    public List<GradeBookDto> GetGradesByCourseId(Guid courseId)
    {
        var gradesCourse = context.GradeBooks.Where(c => c.Id == courseId).ToList();
        return gradesCourse;
    }

    public List<GradeBookDto> GetGradesByUserId(Guid userId)
    {
        var gradesUser = context.GradeBooks.Where(c => c.Id == userId).ToList();
        return gradesUser;
    }

    public void DeactivateGradeBook(GradeBookDto gradeBook)
    {
        gradeBook.IsDeactevated = true;
        context.SaveChanges();
    }
}
