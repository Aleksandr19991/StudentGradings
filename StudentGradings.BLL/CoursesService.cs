using AutoMapper;
using StudentGradings.BLL.Exeptions;
using StudentGradings.BLL.Interfaces;
using StudentGradings.BLL.Mappings;
using StudentGradings.BLL.Models;
using StudentGradings.DAL;
using StudentGradings.DAL.Models.Dtos;

namespace StudentGradings.BLL;

public class CoursesService : ICoursesService
{
    private CoursesRepository _courseRepository;

    private Mapper _mapper;

    public CoursesService()
    {
        _courseRepository = new();

        var config = new MapperConfiguration(
            cfg =>
            {
                cfg.AddProfile(new CourseMapperProfile());
            });
        _mapper = new Mapper(config);
    }
    public UserModelBll GetUsersByCourseId(Guid courseId)
    {
        var users = _courseRepository.GetUsersByCourseId(courseId);
        if (users == null)
            throw new EntityNotFoundException($"Users with id{courseId} was not found");
        var result = _mapper.Map<UserModelBll>(users);
        return result;
    }
    public GradeBookModelBll GetGradesByCourseId(Guid courseId)
    {
        var grades = _courseRepository.GetGradesByCourseId(courseId);
        if (grades == null)
            throw new EntityNotFoundException($"Grades with id{courseId} was not found");
        var result = _mapper.Map<GradeBookModelBll>(grades);
        return result;
    }

    public void AddGradeByCourseId(GradeBookModelBll gradeBook, Guid courseId)
    {
        var grade = _mapper.Map<GradeBookDto>(gradeBook);
        if (grade == null)
            throw new EntityNotFoundException($"Grade with id{courseId} was not found");
        _courseRepository.AddGradeByCourseId(grade, courseId);
    }

    public void UpdateGradeByCourseId(GradeBookModelBll gradeBook)
    {
        var gradeB = _courseRepository.GetGradeBook(gradeBook.CourseId, gradeBook.UserId);
        if (gradeB == null)
            throw new EntityNotFoundException($"GradeBook with course id{gradeBook.CourseId} and user id{gradeBook.UserId}  was not found");
        _courseRepository.UpdateGrade(gradeB, gradeBook.Grade);
    }
}
