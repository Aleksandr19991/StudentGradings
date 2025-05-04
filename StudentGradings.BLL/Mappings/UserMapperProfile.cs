using AutoMapper;
using StudentGradings.BLL.Models;
using StudentGradings.CORE;
using StudentGradings.DAL.Models.Dtos;

namespace StudentGradings.BLL.Mappings;

public class UserMapperProfile : Profile
{
    public UserMapperProfile()
    {
        CreateMap<UserDto, UserModel>()
            .ForMember(dest => dest.UserCourses, opt => opt.MapFrom(src => src.UserCourses));

        CreateMap<UserDto, UserModelWithoutPassword>();

        CreateMap<UserModel, UserDto>()
            .ForMember(dest => dest.UserCourses, opt => opt.MapFrom(src => src.UserCourses));

        CreateMap<UserCourseDto, UserCourseModel>()
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User))
            .ForMember(dest => dest.Course, opt => opt.MapFrom(src => src.Course));

        CreateMap<CourseDto, CourseModel>();
    }
}
