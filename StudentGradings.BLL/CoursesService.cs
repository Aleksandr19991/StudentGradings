using AutoMapper;
using StudentGradings.BLL.Exeptions;
using StudentGradings.BLL.Interfaces;
using StudentGradings.BLL.Mappings;
using StudentGradings.BLL.Models;
using StudentGradings.DAL.Interfaces;
using StudentGradings.DAL.Models.Dtos;

namespace StudentGradings.BLL;

public class CoursesService : ICoursesService
{
    private ICoursesRepository _coursesRepository;
    private Mapper _mapper;

    public CoursesService(ICoursesRepository coursesRepository)
    {
        _coursesRepository = coursesRepository;

        var config = new MapperConfiguration(
            cfg =>
            {
                cfg.AddProfile(new CourseMapperProfile());
            });
        _mapper = new Mapper(config);
    }

    public Guid AddCourse(CourseModelBll courseId)
    {
        var newCourse = _mapper.Map<CourseDto>(courseId);
        if (newCourse == null)
            throw new EntityNotFoundException($"Course with id{courseId} was not found");

        var result = _coursesRepository.AddCourse(newCourse);

        return result;
    }

    public void UpdateCourse(Guid id, CourseModelBll newCourseId)
    {
        var course1 = _coursesRepository.GetCourseById(id);
        if (course1 == null)
            throw new EntityNotFoundException($"Course1 with id{id} was not found");

        var course2 = _mapper.Map<CourseDto>(newCourseId);
        if (course2 == null)
            throw new EntityNotFoundException($"Course2 with id{id} was not found");

        _coursesRepository.UpdateCourse(course1, course2);
    }

    public CourseModelBll GetCourseById(Guid id)
    {
        var course = _coursesRepository.GetCourseById(id);
        if (course == null)
            throw new EntityNotFoundException($"Course with id{id} was not found");

        var result = _mapper.Map<CourseModelBll>(course);
        return result;
    }

    public List<CourseModelBll> GetAllCourses()
    {
        var courses = _coursesRepository.GetAllCourses();
        var result = _mapper.Map<List<CourseModelBll>>(courses);
        return result;
    }

    public UserModelBll GetUsersByCourseId(Guid courseId)
    {
        var users = _coursesRepository.GetUsersByCourseId(courseId);
        if (users == null)
            throw new EntityNotFoundException($"Users with id{courseId} was not found");

        var result = _mapper.Map<UserModelBll>(users);
        return result;
    }

    public void DeactivateCourse(Guid id)
    {
        var course = _coursesRepository.GetCourseById(id);
        if (course == null)
            throw new EntityNotFoundException($"Course with id{id} was not found");

        _coursesRepository.DeactivateCourse(course);
    }

    public void DeleteCourse(Guid id)
    {
        var course = _coursesRepository.GetCourseById(id);
        if (course == null)
            throw new EntityNotFoundException($"Course with id{id} was not found");

        _coursesRepository.DeleteCourse(course);
    }
}
