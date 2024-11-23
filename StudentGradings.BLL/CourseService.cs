using AutoMapper;
using StudentGradings.BLL.Mappings;
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
}
