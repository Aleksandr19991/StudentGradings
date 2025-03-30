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

    public async Task<Guid> AddCourseAsync(CourseModel courseId)
    {
        var newCourse = _mapper.Map<CourseDto>(courseId);
        if (newCourse == null)
            throw new EntityNotFoundException($"Course with id{courseId} was not found.");

        var result = await _coursesRepository.AddCourseAsync(newCourse);

        return result;
    }

    public async Task UpdateCourseAsync(Guid id, CourseModel newCourseId)
    {
        var course = await _coursesRepository.GetCourseByIdAsync(id);
        if (course == null)
            throw new EntityNotFoundException($"Course with id{id} was not found.");

        var newCourse = _mapper.Map<CourseDto>(newCourseId);
        if (newCourse == null)
            throw new EntityNotFoundException($"NewCourse with id{id} was not found.");

        await _coursesRepository.UpdateCourseAsync(course, newCourse);
    }

    public async Task<CourseModel> GetCourseByIdAsync(Guid id)
    {
        var course = await _coursesRepository.GetCourseByIdAsync(id);
        if (course == null)
            throw new EntityNotFoundException($"Course with id{id} was not found.");

        var result = _mapper.Map<CourseModel>(course);
        return result;
    }

    public async Task<List<CourseModel>> GetAllCoursesAsync()
    {
        var courses = await _coursesRepository.GetAllCoursesAsync();
        if (courses == null)
            throw new EntityNotFoundException($"Courses was not found.");
        var result = _mapper.Map<List<CourseModel>>(courses);
        return result;
    }

    public async Task<CourseModel> GetCourseWithUsersAndGradesAsync(Guid courseId)
    {
        var course = await _coursesRepository.GetCourseWithUsersAndGradesAsync(courseId);
        if (course == null)
            throw new EntityNotFoundException($"Course with id{courseId} was not found.");

        var result = _mapper.Map<CourseModel>(course);
        return result;
    }

    public async Task DeactivateCourseAsync(Guid id)
    {
        var course = await _coursesRepository.GetCourseByIdAsync(id);
        if (course == null)
            throw new EntityNotFoundException($"Course with id{id} was not found.");

        await _coursesRepository.DeactivateCourseAsync(course);
    }

    public async Task DeleteCourseAsync(Guid id)
    {
        var course = await _coursesRepository.GetCourseByIdAsync(id);
        if (course == null)
            throw new EntityNotFoundException($"Course with id{id} was not found.");

        await _coursesRepository.DeleteCourseAsync(course);
    }
}
