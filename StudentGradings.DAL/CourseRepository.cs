using Microsoft.EntityFrameworkCore;
using StudentGradings.DAL.Models.Dtos;

namespace StudentGradings.DAL
{
    public class CourseRepository
    {
        private Context _context;

        public CourseRepository()
        {
            _context = new Context();
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

        public IEnumerable<UserDto> GetUsersByCourseId(Guid courseId)
        {
            var students = _context.Courses.Include(c => c.Users).Where(c => c.Id == courseId).FirstOrDefault();
            return _context.Users.ToList();
        }

        public IEnumerable<CourseDto> GetGradesByCourseId(Guid courseId)
        {
            var grades = _context.GradeBooks.Include(g => g.Grade).Where(c => c.Id == courseId).ToList();
            return _context.Courses.ToList();
        }

        public void AddGradeByCourseId(GradeBookDto gradeBook, Guid courseId)
        {
            var grade = _context.GradeBooks.Include(s => s.Grade).Where(c => c.Id == courseId).SingleOrDefault();
            _context.GradeBooks.Add(grade);
            _context.SaveChanges();
        }

        public void UpdateGradeByCourseId(GradeBookDto gradeBook, Guid courseId)
        {
            var grade = _context.GradeBooks.Include(s => s.Grade).Where(c => c.Id == courseId).SingleOrDefault();
            _context.GradeBooks.Update(grade);
            _context.SaveChanges();
        }
    }
}
