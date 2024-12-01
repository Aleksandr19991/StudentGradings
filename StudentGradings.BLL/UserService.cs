using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using StudentGradings.BLL.Exeptions;
using StudentGradings.BLL.Mappings;
using StudentGradings.BLL.Models;
using StudentGradings.CORE;
using StudentGradings.DAL;
using StudentGradings.DAL.Models.Dtos;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StudentGradings.BLL;

public class UserService
{
    private UserRepository _userRepository;

    private Mapper _mapper;

    public UserService()
    {
        _userRepository = new();

        var config = new MapperConfiguration(
            cfg =>
            {
                cfg.AddProfile(new UserMapperProfile());
            });
        _mapper = new Mapper(config);
    }

    public string? Authenticate(string email, string password)
    {
        var user = _userRepository.GetUserByEmail(email);

        if (user != null && user.Password == password)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConstantAuth.Key));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokeOptions = new JwtSecurityToken(
            issuer: ConstantAuth.Issuer,
            audience: ConstantAuth.Audience,
            claims: new List<Claim>(),
            expires: DateTime.Now.AddMinutes(5),
            signingCredentials: signinCredentials
            );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            return tokenString;
        }
        else return null;
    }

    public void AddUser(UserModelBll user)
    {
        var userId = _mapper.Map<UserDto>(user);
        if (userId == null)
            throw new EntityNotFoundException($"User with id{user} was not found");
        _userRepository.AddUser(userId);
    }

    public void UpdateUser(UserModelBll user)
    {
        var userId = _mapper.Map<UserDto>(user);
        if (userId == null)
            throw new EntityNotFoundException($"User with id{user} was not found");
        _userRepository.UpdateUser(userId);
    }

    public UserModelBll GetUserRoleByUserId(Guid userId)
    {
        var role = _userRepository.GetUserRoleByUserId(userId);
        if (role == null)
            throw new EntityNotFoundException($"Role with id{userId} was not found");
        var result = _mapper.Map<UserModelBll>(role);
        return result;
    }

    public IEnumerable<UserModelBll> GetCoursesByUserId(Guid userId)
    {
        var courses = _userRepository.GetCoursesByUserId(userId);
        if (courses == null)
            throw new EntityNotFoundException($"Courses with id{userId} was not found");
        var result = _mapper.Map<IEnumerable<UserModelBll>>(courses);
        return result;
    }
}
