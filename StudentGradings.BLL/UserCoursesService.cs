using AutoMapper;
using StudentGradings.BLL.Exeptions;
using StudentGradings.BLL.Interfaces;
using StudentGradings.BLL.Mappings;
using StudentGradings.BLL.Models;
using StudentGradings.DAL.Interfaces;

namespace StudentGradings.BLL;

public class UserCoursesService : IUserCoursesService
{
    private ICoursesRepository _coursesRepository;
    private IUsersRepository _usersRepository;
    private IUserCoursesRepository _userCoursesRepository;
    private Mapper _mapper;

    public UserCoursesService(
        IUserCoursesRepository userCoursesRepository,
        ICoursesRepository coursesRepository,
        IUsersRepository usersRepository
    )
    {
        _coursesRepository = coursesRepository;
        _usersRepository = usersRepository;
        _userCoursesRepository = userCoursesRepository;

        var config = new MapperConfiguration(
            cfg =>
            {
                cfg.AddProfile(new CourseMapperProfile());
                cfg.AddProfile(new UserMapperProfile());
                cfg.AddProfile(new UserCourseMapperProfile());
            });
        _mapper = new Mapper(config);
    }

    public async Task AddGradeByUserIdAndCourseIdAsync(Guid userId, Guid courseId, float grade)
    {
        var user = await _usersRepository.GetUserByIdAsync(userId);
        if (user == null)
            throw new EntityNotFoundException($"User with id {userId} was not found.");

        if (user.IsDeactivated)
            throw new EntityConflictException($"User with id {userId} is deactivated.");

        var course = await _coursesRepository.GetCourseByIdAsync(courseId);
        if (course == null)
            throw new EntityNotFoundException($"Course with id {courseId} was not found.");

        if (course.IsDeactivated)
            throw new EntityConflictException($"Course with id {courseId} is deactivated.");

        if (grade < 0 || grade > 5)
            throw new EntityConflictException($"Grade {grade}, must be between 0 and 5");

        await _userCoursesRepository.AddGradeByUserIdAndCourseIdAsync(userId, courseId, grade);
    }

    public async Task UpdateGradeByCourseIdAndUserIdAsync(Guid userId, Guid courseId, float grade)
    {
        var book = await _userCoursesRepository.GetUserCourseAsync(courseId, userId);
        if (book == null)
            throw new EntityNotFoundException($"UserCourse with user {userId} and course {courseId} was not found.");

        await _userCoursesRepository.UpdateGradeByCourseIdAndUserIdAsync(userId,courseId, grade);
    }

    public async Task<UserCourseModel> GetUserCourseAsync(Guid courseId, Guid userId)
    {
        var userCourse = await _userCoursesRepository.GetUserCourseAsync(courseId, userId);
        if (userCourse == null)
            throw new EntityNotFoundException($"UserCourse with id {courseId} and with id {userId} was not found.");

        return _mapper.Map<UserCourseModel>(userCourse);
    }

    public async Task<List<UserCourseModel>> GetGradesByCourseIdAsync(Guid userId, Guid courseId)
    {
        var gradesCourse = await _userCoursesRepository.GetGradesByCourseIdAsync(userId, courseId);
        if (gradesCourse == null)
            throw new EntityNotFoundException($"GradeCourse with id {courseId} was not found.");

        return _mapper.Map<List<UserCourseModel>>(gradesCourse);
    }

    public async Task<List<UserCourseModel>> GetAllGradesByUserIdAsync(Guid userId)
    {
        var allGrades = await _userCoursesRepository.GetAllGradesByUserIdAsync(userId);
        if (allGrades == null || !allGrades.Any())
            throw new EntityNotFoundException($"Grades was not found.");

        return _mapper.Map<List<UserCourseModel>>(allGrades);
    }

    public async Task DeleteGradeByCourseIdAndUserIdAsync(Guid userId, Guid courseId)
    {
        var gradeExists = await _userCoursesRepository.GradeExistsByCourseIdAndUserIdAsync(userId, courseId);

        if (gradeExists)
        {
            await _userCoursesRepository.DeleteGradeByCourseIdAndUserIdAsync(userId, courseId);
        }
        else
        {
            throw new EntityNotFoundException($"Grade for user {userId} and course {courseId}  not found.");
        }
    }
}