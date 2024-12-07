using Microsoft.EntityFrameworkCore;
using StudentGradings.DAL.Interfaces;
using StudentGradings.DAL.Models.Dtos;

namespace StudentGradings.DAL
{
    public class CoursesRepository(StudentGradingsContext context) : ICoursesRepository
    {
        public Guid AddCourse(CourseDto course)
        {
            context.Courses.Add(course);
            context.SaveChanges();
            return course.Id;
        }

        public void UpdateCourse(CourseDto course, CourseDto changeCourse)
        {
            course.Name = changeCourse.Name;
            course.Description = changeCourse.Description;
            course.Hours = changeCourse.Hours;
            context.SaveChanges();
        }

        public CourseDto? GetCourseById(Guid id) => context.Courses.SingleOrDefault(c => c.Id == id);

        public List<UserDto> GetUsersByCourseId(Guid courseId)
        {
            var course = context.Courses.Where(c => c.Id == courseId).Include(c => c.Users).FirstOrDefault();
            var students = course.Users.ToList();
            return students;
        }

        public void DeactivateUser(CourseDto course)
        {
            course.IsDeactevated = true;
            context.SaveChanges();
        }

        public void DeleteCourse(CourseDto course)
        {
            context.Courses.Remove(course);
            context.SaveChanges();
        }
    }
}
