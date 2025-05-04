using AutoMapper;
using StudentGradings.API.Models.Requests;
using StudentGradings.API.Models.Responses;
using StudentGradings.BLL.Models;

namespace StudentGradings.API.Mappings
{
    public class CourseMapperProfile : Profile
    {
        public CourseMapperProfile()
        {
            CreateMap<CreateCourseRequest, CourseModel>();
            CreateMap<CourseModel, CourseResponse>();
            CreateMap<CourseModel, CourseWithGradeResponse>();
            CreateMap<UpdateCourseRequest, CourseModel>();
            CreateMap<CreateGradeRequest, UserCourseModel>();
            CreateMap<UserCourseModel, UserCourseResponse>();
        }
    }
}
