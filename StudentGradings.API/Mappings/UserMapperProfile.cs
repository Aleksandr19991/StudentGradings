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
        CreateMap<UserModel, UserResponse>()
             .ForMember(dest => dest.Courses,
                opt => opt.MapFrom(src => src.UserCourses.Select(uc => new CourseWithGradeResponse
                {
                    Id = uc.Course.Id,
                    Name = uc.Course.Name,
                    Description = uc.Course.Description,
                    Hours = uc.Course.Hours,
                    Semester = uc.Course.Semester,
                    Grade = uc.Grade,
                    DateAssigned = uc.DateAssigned
                })));
        CreateMap<LoginRequest, UserModel>();
        CreateMap<UserModel, AuthenticatedResponse>();
        CreateMap<UpdateUserRequest, UserModel>();
        CreateMap<UserRole, UserModel>();
    }
}
