using StudentGradings.DAL.Models.Dtos;
using Microsoft.EntityFrameworkCore;

namespace StudentGradings.DAL
{
    public class CourseRepository
    {
        Context context = new();

        public void AddCourse(CourseDto course)
        {
            context.Courses.Add(course);
            context.SaveChanges();
        }

        public IEnumerable<StudentDto> GetStudentsByCourseId(Guid courseId) //teacher
        {
            var students = context.Students.Include(s => s.Course).Where(c => c.Course.Id == courseId).FirstOrDefault();
            return context.Students.ToList();
        }

        public StudentDto GetGradeByCourseId(Guid courseId) //student
        {
            var grade = context.Students.Include(g => g.Grade).Where(c => c.Id == courseId).FirstOrDefault();
            return grade;
        }

        public IEnumerable<CourseDto> GetGradesByAllCourses(StudentDto student) //student
        {
            var grades = context.Courses.Include(c => c.Student).ThenInclude(s => s.Grade).ToList();
            return context.Courses.ToList();
        }

        public void AddGradeByCourseId(Guid courseId) //teacher
        {
            var grade = context.Students.Include(s => s.Grade).Where(c => c.Id == courseId).SingleOrDefault();
            context.Students.Add(grade);
            context.SaveChanges();
        }

        public void UpdateGradeByCourseId(Guid courseId) //teacher
        {
            var grade = context.Students.Include(s => s.Grade).Where(c => c.Id == courseId).SingleOrDefault();
            context.Students.Update(grade);
            context.SaveChanges();
        }
    }
}
