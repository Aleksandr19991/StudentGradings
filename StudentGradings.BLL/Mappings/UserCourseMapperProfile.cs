using AutoMapper;
using StudentGradings.BLL.Models;
using StudentGradings.DAL.Models.Dtos;

namespace StudentGradings.BLL.Mappings;

public class UserCourseMapperProfile : Profile
{
    public UserCourseMapperProfile()
    {
        CreateMap<UserCourseModel, UserCourseDto>();
        CreateMap<UserCourseDto, UserCourseModel>();
    }
}
