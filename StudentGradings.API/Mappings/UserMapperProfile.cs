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
        CreateMap<RegisterUserRequest, UserModel>();
        CreateMap<UserModel, UserResponse>();
        CreateMap<UserModel, UserWithCoursesAndGradesResponse>();
        CreateMap<LoginRequest, UserModel>();
        CreateMap<UserModel, AuthenticatedResponse>();
        CreateMap<AddGradeRequest, GradeBookResponse>();
        CreateMap<UserRole, UserModel>();
        CreateMap<UserModel, UserResponse>();
    }
}
