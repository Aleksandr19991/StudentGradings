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

    public async Task<Guid> AddCourseAsync(CourseModel courseModel)
    {
        if (courseModel == null)
            throw new EntityNotFoundException($"CourseModel not found.");

        var newCourse = _mapper.Map<CourseDto>(courseModel);
        return await _coursesRepository.AddCourseAsync(newCourse);
    }

    public async Task UpdateCourseAsync(Guid id, CourseModel courseModel)
    {
        if (courseModel == null)
            throw new EntityNotFoundException($"CourseModel not found.");

        var existingCourse = await _coursesRepository.GetCourseByIdAsync(id);
        if (existingCourse == null)
            throw new EntityNotFoundException($"Course with id {id} not found.");

        var newCourseDto = _mapper.Map<CourseDto>(courseModel);
        await _coursesRepository.UpdateCourseAsync(existingCourse, newCourseDto);
    }

    public async Task<CourseModel> GetCourseByIdAsync(Guid id)
    {
        var course = await _coursesRepository.GetCourseByIdAsync(id);
        if (course == null)
            throw new EntityNotFoundException($"Course with id {id} was not found.");

        return _mapper.Map<CourseModel>(course);
    }

    public async Task<List<CourseModel>> GetAllCoursesAsync()
    {
        var courses = await _coursesRepository.GetAllCoursesAsync();
        return _mapper.Map<List<CourseModel>>(courses);
    }

    public async Task<CourseModel> GetCourseWithUsersAndGradesAsync(Guid courseId)
    {
        var course = await _coursesRepository.GetCourseWithUsersAndGradesAsync(courseId);
        if (course == null)
            throw new EntityNotFoundException($"Course with id {courseId} was not found.");

        return _mapper.Map<CourseModel>(course);
    }

    public async Task DeactivateCourseAsync(Guid id)
    {
        var course = await _coursesRepository.GetCourseByIdAsync(id);
        if (course == null)
            throw new EntityNotFoundException($"Course with id {id} was not found.");

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