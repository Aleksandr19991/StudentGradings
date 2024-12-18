using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using StudentGradings.BLL.Exeptions;
using StudentGradings.BLL.Interfaces;
using StudentGradings.BLL.Mappings;
using StudentGradings.BLL.Models;
using StudentGradings.CORE;
using StudentGradings.DAL.Interfaces;
using StudentGradings.DAL.Models.Dtos;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StudentGradings.BLL;

public class UsersService : IUsersService
{
    private IUsersRepository _usersRepository;
    private Mapper _mapper;

    public UsersService(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;

        var config = new MapperConfiguration(
            cfg =>
            {
                cfg.AddProfile(new UserMapperProfile());
            });
        _mapper = new Mapper(config);
    }

    public string? Authenticate(string email, string password)
    {
        var user = _usersRepository.GetUserByEmail(email);

        if (user != null && user.Password == password)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, "42"),
                new Claim(ClaimTypes.Role, "Teacher"),
                new Claim(ClaimTypes.Role, "Student"),
                new Claim(ClaimTypes.Role, "Administrator")
            };

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
        else
            throw new EntityNotFoundException($"The Email or Password is entered incorrectly, please try again");
    }

    public Guid AddUser(UserModelBll userId)
    {
        var newUser = _mapper.Map<UserDto>(userId);
        if (newUser == null)
            throw new EntityNotFoundException($"User with id{userId} was not found.");

        var result = _usersRepository.AddUser(newUser);
        return result;
    }

    public void UpdateUser(Guid id, UserModelBll newUser)
    {
        var user1 = _usersRepository.GetUserById(id);
        if (user1 == null)
            throw new EntityNotFoundException($"User1 with id{id} was not found.");

        var user2 = _mapper.Map<UserDto>(newUser);
        if (user2 == null)
            throw new EntityNotFoundException($"User2 with id{id} was not found.");

        _usersRepository.UpdateUser(user1, user2);
    }

    public void UpdatePasswordByUserId(Guid id, UserModelBll user)
    {
        var userPassword = _usersRepository.GetUserById(id);
        if (userPassword == null)
            throw new EntityNotFoundException($"User with id{id} was not found.");

        _usersRepository.UpdatePasswordByUserId(userPassword, user.Password);
    }

    public List<UserModelBll> GetAllUsers()
    {
        var users = _usersRepository.GetAllUsers();
        var result = _mapper.Map<List<UserModelBll>>(users);
        return result;
    }

    public UserModelBll GetCoursesByUserId(Guid userId)
    {
        var courses = _usersRepository.GetCoursesByUserId(userId);
        if (courses == null)
            throw new EntityNotFoundException($"Courses with id{userId} was not found.");
        var result = _mapper.Map<UserModelBll>(courses);
        return result;
    }

    public void DeactivateUser(Guid id)
    {
        var user = _usersRepository.GetUserById(id);
        if (user == null)
            throw new EntityNotFoundException($"User with id{id} was not found.");

        _usersRepository.DeactivateUser(user);
    }

    public void DeleteUser(Guid id)
    {
        var user = _usersRepository.GetUserById(id);
        if (user == null)
            throw new EntityNotFoundException($"User with id{id} was not found.");

        _usersRepository.DeleteUser(user);
    }
}
