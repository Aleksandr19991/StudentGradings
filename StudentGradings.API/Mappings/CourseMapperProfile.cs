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
            CreateMap<CreateCourseRequest,CourseModelBll>();
            CreateMap<CourseModelBll, CourseResponse>();
            CreateMap<CourseModelBll, CourseWithUsersResponse>();
        }
    }
}
