using Microsoft.EntityFrameworkCore;
using StudentGradings.DAL.Interfaces;
using StudentGradings.DAL.Models.Dtos;

namespace StudentGradings.DAL
{
    public class CoursesRepository : ICoursesRepository
    {
        private StudentGradingsContext _context;

        public CoursesRepository()
        {
            //_context = new Context();
        }

        public void AddCourse(CourseDto course)
        {
            _context.Courses.Add(course);
            _context.SaveChanges();
        }

        public void UpdateCourse(CourseDto course)
        {
            _context.Courses.Update(course);
            _context.SaveChanges();
        }

        public List<UserDto> GetUsersByCourseId(Guid courseId)
        {
            var course = _context.Courses.Where(c => c.Id == courseId).Include(c => c.Users).FirstOrDefault();
            var students = course.Users.ToList();
            return students;
        }

        public List<GradeBookDto> GetGradesByCourseId(Guid courseId)
        {
            var grades = _context.GradeBooks.Where(c => c.Id == courseId).ToList(); 
            return grades;
        }

        public void AddGradeByCourseId(GradeBookDto gradeBook)
        {
            _context.GradeBooks.Add(gradeBook);
            _context.SaveChanges();
        }

        public void UpdateGrade(GradeBookDto gradeBook, float grade)
        {
           gradeBook.Grade = grade;
            _context.SaveChanges();
        }

        public GradeBookDto GetGradeBook(Guid courseId, Guid userId)
        {
            var gradeBook = _context.GradeBooks.Where(c => c.Course.Id == courseId && c.User.Id == userId).SingleOrDefault();
            return gradeBook;
        }
    }
}
