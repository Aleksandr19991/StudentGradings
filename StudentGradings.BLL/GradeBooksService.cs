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

    public async Task<Guid> AddGradeBookAsync(GradeBookModel gradeBookModel)
    {
        var newGradeBook = _mapper.Map<GradeBookDto>(gradeBookModel);
        if (newGradeBook == null)
            throw new EntityNotFoundException($"GradeBook {gradeBookModel} was not found.");

        return await _gradeBooksRepository.AddGradeBookAsync(newGradeBook);
    }

    public async Task AddGradeByCourseIdAsync(Guid courseId, Guid userId)
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
            User = user
        };

        await _gradeBooksRepository.AddGradeByCourseIdAsync(newGradeBook);
    }

    public async Task UpdateGradeByCourseIdAsync(Guid courseId, Guid userId, GradeBookModel gradeBook)
    {
        var book = await _gradeBooksRepository.GetGradeBookAsync(courseId, userId);
        if (book == null)
            throw new EntityNotFoundException($"GradeBook with course id {gradeBook.CourseId} and user id {gradeBook.UserId} was not found.");

        var newGradeBook = _mapper.Map<GradeBookDto>(gradeBook);
        await _gradeBooksRepository.UpdateGradeByCourseIdAsync(book, newGradeBook.Grade);
    }

    public async Task<GradeBookModel> GetGradeBookAsync(Guid courseId, Guid userId)
    {
        var gradebook = await _gradeBooksRepository.GetGradeBookAsync(courseId, userId);
        if (gradebook == null)
            throw new EntityNotFoundException($"Gradebook with id {courseId} and with id {userId} was not found.");

        return _mapper.Map<GradeBookModel>(gradebook);
    }

    public async Task<IEnumerable<GradeBookModel>> GetGradesByCourseIdAsync(Guid courseId)
    {
        var grades = await _gradeBooksRepository.GetGradesByCourseIdAsync(courseId);
        if (grades == null || !grades.Any())
            return Enumerable.Empty<GradeBookModel>();

        var result = grades.Select(g => _mapper.Map<GradeBookModel>(g));
        return result;
    }
}
