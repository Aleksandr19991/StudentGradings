using AutoMapper;
using StudentGradings.BLL.Exeptions;
using StudentGradings.BLL.Interfaces;
using StudentGradings.BLL.Mappings;
using StudentGradings.BLL.Models;
using StudentGradings.DAL.Interfaces;
using StudentGradings.DAL.Models.Dtos;

namespace StudentGradings.BLL;

public class GradeBooksService : IGradeBooksService
{
    private ICoursesRepository _coursesRepository;
    private IUsersRepository _usersRepository;
    private IGradeBooksRepository _gradeBooksRepository;
    private Mapper _mapper;

    public GradeBooksService(
        IGradeBooksRepository gradeBooksRepository,
        ICoursesRepository coursesRepository,
        IUsersRepository usersRepository
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

    public async Task AddGradeByCourseIdAndUserIdAsync(Guid courseId, Guid userId, float grade)
    {
        var gradeBook = await _gradeBooksRepository.GetGradeBookAsync(courseId, userId);
        if (gradeBook != null)
            throw new EntityConflictException($"Grade with user id {userId} and course id {courseId} already exists.");

        var course = await _coursesRepository.GetCourseByIdAsync(courseId);
        if (course == null)
            throw new EntityNotFoundException($"Course with id {courseId} was not found.");

        if (course.IsDeactivated)
            throw new EntityConflictException($"Course with id {courseId} is deactivated.");

        var user = await _usersRepository.GetUserByIdAsync(userId);
        if (user == null)
            throw new EntityNotFoundException($"User with id {userId} was not found.");

        if (user.IsDeactivated)
            throw new EntityConflictException($"User with id {userId} is deactivated.");

        var newGradeBook = new GradeBookDto()
        {
            Course = course,
            User = user,
            Grade = grade
        };

        await _gradeBooksRepository.AddGradeByCourseIdAndUserIdAsync(newGradeBook);
    }

    public async Task UpdateGradeByCourseIdAndUserIdAsync(Guid courseId, Guid userId, float grade)
    {
        var book = await _gradeBooksRepository.GetGradeBookAsync(courseId, userId);
        if (book == null)
            throw new EntityNotFoundException($"GradeBook with course {courseId} and user {userId} was not found.");

        await _gradeBooksRepository.UpdateGradeByCourseIdAndUserIdAsync(book, grade);
    }

    public async Task<GradeBookModel> GetGradeBookAsync(Guid courseId, Guid userId)
    {
        var gradebook = await _gradeBooksRepository.GetGradeBookAsync(courseId, userId);
        if (gradebook == null)
            throw new EntityNotFoundException($"Gradebook with id {courseId} and with id {userId} was not found.");

        return _mapper.Map<GradeBookModel>(gradebook);
    }

    public async Task<List<GradeBookModel>> GetGradesByCourseIdAsync(Guid courseId)
    {
        var gradesCourse = await _gradeBooksRepository.GetGradesByCourseIdAsync(courseId);
        if (gradesCourse == null)
            throw new EntityNotFoundException($"GradeCourse with id {courseId} was not found.");

        return _mapper.Map<List<GradeBookModel>>(gradesCourse);
    }

    public async Task<List<GradeBookModel>> GetAllGradesByCoursesAsync()
    {
        var allGrades = await _gradeBooksRepository.GetAllGradesWithCoursesAsync();
        if (allGrades == null || !allGrades.Any())
            throw new EntityNotFoundException($"Grades was not found.");

        return _mapper.Map<List<GradeBookModel>>(allGrades);
    }

    public async Task DeleteGradeByCourseIdAndUserIdAsync(Guid courseId, Guid userId)
    {
        var gradeExists = await _gradeBooksRepository.GradeExistsByCourseIdAndUserIdAsync(courseId, userId);

        if (gradeExists)
        {
            await _gradeBooksRepository.DeleteGradeByCourseIdAndUserIdAsync(courseId, userId);
        }
        else
        {
            throw new EntityNotFoundException($"Grade for course {courseId} and user {userId} not found.");
        }
    }
}