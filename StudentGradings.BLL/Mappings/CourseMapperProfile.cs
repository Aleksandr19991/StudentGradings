using AutoMapper;
using StudentGradings.BLL.Models;
using StudentGradings.DAL.Models.Dtos;

namespace StudentGradings.BLL.Mappings;

public class CourseMapperProfile : Profile
{
    public CourseMapperProfile()
    {
        CreateMap<CourseModel, CourseDto>();
        CreateMap<CourseDto, CourseModel>();
        CreateMap<CourseDto, CourseModelShort>();
    }
}
