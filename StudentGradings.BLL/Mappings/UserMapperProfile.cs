using AutoMapper;
using StudentGradings.BLL.Models;
using StudentGradings.DAL.Models.Dtos;

namespace StudentGradings.BLL.Mappings;

public class UserMapperProfile : Profile
{
    public UserMapperProfile()
    {
        CreateMap<UserModelBll, UserDto>();
        CreateMap<UserDto, UserModelBll>();
        CreateMap<UserRoleModelBll, UserRoleDto>();
        CreateMap<UserRoleDto, UserRoleModelBll>();
    }
}
