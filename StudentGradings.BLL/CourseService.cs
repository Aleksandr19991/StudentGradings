using AutoMapper;
using StudentGradings.BLL.Mappings;
using StudentGradings.BLL.Models;
using StudentGradings.DAL;
using StudentGradings.DAL.Models.Dtos;

namespace StudentGradings.BLL;

public class CourseService
{
    private CourseRepository _courseRepository;

    private Mapper _mapper;

    public CourseService()
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
        var result = _mapper.Map<UserModelBll>(users);
        return result;
    }
    public GradeBookModelBll GetGradesByCourseId(Guid courseId)
    {
        var grades = _courseRepository.GetGradesByCourseId(courseId);
        var result = _mapper.Map<GradeBookModelBll>(grades);
        return result;
    }

    public void AddGradeByCourseId(GradeBookModelBll gradeBook, Guid courseId)
    {
        var grade = _mapper.Map<GradeBookDto>(gradeBook);
        _courseRepository.AddGradeByCourseId(grade,courseId);
    }

    public void UpdateGradeByCourseId(GradeBookModelBll gradeBook, Guid courseId)
    {
        var grade = _mapper.Map<GradeBookDto>(gradeBook);
        _courseRepository.UpdateGradeByCourseId(grade, courseId);
    }
}
