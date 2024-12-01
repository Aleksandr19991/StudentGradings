using AutoMapper;
using StudentGradings.BLL.Models;
using StudentGradings.DAL.Models.Dtos;

namespace StudentGradings.BLL.Mappings;

public class GradeBookProfile : Profile
{
    public GradeBookProfile()
    {
        CreateMap<GradeBookModelBll, GradeBookDto>();
        CreateMap<GradeBookDto, GradeBookModelBll>();
    }
}
