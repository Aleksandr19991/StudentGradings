using AutoMapper;
using StudentGradings.BLL.Exeptions;
using StudentGradings.BLL.Mappings;
using StudentGradings.BLL.Models;
using StudentGradings.DAL;

namespace StudentGradings.BLL;

public class CourseService
{
    public CourseRepository CourseRepository { get; set; }

    private Mapper _mapper;

    public CourseService()
    {
        CourseRepository = new();

        var config = new MapperConfiguration(
            cfg =>
            {
                cfg.AddProfile(new CourseMapperProfile());
            });
        _mapper = new Mapper(config);
    }

    //public GradeBookModelBll GetGradeByCourseId(Guid courseId)
    //{
    //    var grade = CourseRepository.GetGradeByCourseId(courseId);

    //    if (grade == null)
    //    {
    //        throw new EntityNotFoundException($"Grade with id{courseId} not found");
    //    }

    //    var newGrade = new Grade()
    //    {
    //        Grade = grade,
    //    };

    //    return grade;
    //}
}
