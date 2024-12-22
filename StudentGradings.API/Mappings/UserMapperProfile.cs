using AutoMapper;
using StudentGradings.API.Models.Requests;
using StudentGradings.API.Models.Responses;
using StudentGradings.BLL.Models;
using StudentGradings.CORE;

namespace StudentGradings.API.Mappings;

public class UserMapperProfile : Profile
{
    public UserMapperProfile()
    {
        CreateMap<RegisterUserRequest, UserModelBll>();
        CreateMap<UserModelBll, UserResponse>();
        CreateMap<UserModelBll, UserWithCoursesAndGradesResponse>();
        CreateMap<LoginRequest, UserModelBll>();
        CreateMap<UserModelBll, AuthenticatedResponse>();
        CreateMap<AddGradeRequest, GradeBookResponse>();
        CreateMap<UserRole, UserModelBll>();
        CreateMap<UserModelBll, UserResponse>();
    }
}
