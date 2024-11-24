using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using StudentGradings.BLL.Mappings;
using StudentGradings.CORE;
using StudentGradings.DAL;
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

    public string? Authenticate (string email, string password)
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
}
