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
    private IGradeBooksRepository _gradeBooksRepository;
    private IUsersRepository _usersRepository;

    private Mapper _mapper;

    public CoursesService(
        ICoursesRepository coursesRepository,
        IUsersRepository usersRepository,
        IGradeBooksRepository gradeBooksRepository
    )
    {
        _coursesRepository = coursesRepository;
        _usersRepository = usersRepository;
        _gradeBooksRepository = gradeBooksRepository;

        var config = new MapperConfiguration(
            cfg =>
            {
                cfg.AddProfile(new CourseMapperProfile());
                cfg.AddProfile(new UserMapperProfile());
                cfg.AddProfile(new GradeBookProfile());
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
    public Guid AddGradeBook(GradeBookModelBll gradeBookId)
    {
        var newGradeBook = _mapper.Map<GradeBookDto>(gradeBookId);
        if (newGradeBook == null)
            throw new EntityNotFoundException($"Course with id{gradeBookId} was not found");

        var result = _gradeBooksRepository.AddGradeBook(newGradeBook);

        return result;
    }

    public void AddGradeByCourseId(Guid courseId, Guid userId)
    {
        var course = _coursesRepository.GetCourseById(courseId);
        if (course == null)
            throw new EntityNotFoundException($"Course with id{courseId} was not found");

        var user = _usersRepository.GetUserById(userId);
        if (user == null)
            throw new EntityNotFoundException($"User with id{userId} was not found");

        var newGradeBook = new GradeBookDto()
        {
            Course = course,
            User = user
        };

        _gradeBooksRepository.AddGradeByCourseId(newGradeBook);
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

    public void UpdateGradeByCourseId(GradeBookModelBll gradeBook)
    {
        var gradeCourse = _gradeBooksRepository.GetGradeBook(gradeBook.CourseId, gradeBook.UserId);
        if (gradeCourse == null)
            throw new EntityNotFoundException($"GradeBook with course id{gradeBook.CourseId} and user id{gradeBook.UserId}  was not found");

        _gradeBooksRepository.UpdateGrade(gradeCourse, gradeBook.Grade);
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
    public GradeBookModelBll GetGradesByCourseId(Guid courseId)
    {
        var grades = _gradeBooksRepository.GetGradesByCourseId(courseId);
        if (grades == null)
            throw new EntityNotFoundException($"Grades with id{courseId} was not found");

        var result = _mapper.Map<GradeBookModelBll>(grades);
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
