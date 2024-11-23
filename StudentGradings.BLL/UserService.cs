using AutoMapper;
using StudentGradings.BLL.Mappings;
using StudentGradings.DAL;

namespace StudentGradings.BLL;

public class UserService
{
    public UserRepository UserRepository { get; set; }

    private Mapper _mapper;

    public UserService()
    {
        UserRepository = new();

        var config = new MapperConfiguration(
            cfg =>
            {
                cfg.AddProfile(new UserMapperProfile());
            });
        _mapper = new Mapper(config);
    }
}
