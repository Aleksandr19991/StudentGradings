using AutoMapper;
using StudentGradings.BLL.Models;
using StudentGradings.CORE;
using StudentGradings.DAL.Models.Dtos;

namespace StudentGradings.BLL.Mappings;

public class UserMapperProfile : Profile
{
    public UserMapperProfile()
    {
        CreateMap<UserModel, UserDto>();
        CreateMap<UserDto, UserModel>();
        CreateMap<UserRole, UserDto>();
        CreateMap<UserDto, UserRole>();
    }
}
