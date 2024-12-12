using AutoMapper;
using StudentGradings.API.Models;
using StudentGradings.API.Models.Requests;
using StudentGradings.API.Models.Responses;
using StudentGradings.BLL.Models;

namespace StudentGradings.API.Mappings;

public class UserMapperProfile : Profile
{
    public UserMapperProfile()
    {
        CreateMap<RegisterUserRequest, UserModelBll>();
        CreateMap<UserModelBll, UserResponse>();
        CreateMap<UserModelBll, UserWithCoursesResponse>();
        CreateMap<LoginRequest, UserModelBll>();
        CreateMap<UserModelBll, AuthenticatedResponse>();
        CreateMap<AddGradeRequest, GradeBookResponse>();
        CreateMap<UserRole, UserRoleModelBll>();
        CreateMap<UserModelBll, UserResponse>();
    }
}
