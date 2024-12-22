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

    public async Task<string?> AuthenticateAsync(string email, string password)
    {
        var user = await _usersRepository.GetUserByEmailAsync(email);

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

    public async Task<Guid> AddUserAsync(UserModelBll userId)
    {
        var newUser = _mapper.Map<UserDto>(userId);
        if (newUser == null)
            throw new EntityNotFoundException($"User with id{userId} was not found.");

        var result = await _usersRepository.AddUserAsync(newUser);
        return result;
    }

    public async Task UpdateUserAsync(Guid id, UserModelBll User)
    {
        var user = await _usersRepository.GetUserByIdAsync(id);
        if (user == null)
            throw new EntityNotFoundException($"User with id{id} was not found.");

        var newUser = _mapper.Map<UserDto>(User);
        if (newUser == null)
            throw new EntityNotFoundException($"newUser with id{id} was not found.");

        await _usersRepository.UpdateUserAsync(user, newUser);
    }

    public async Task UpdatePasswordByUserIdAsync(Guid id, UserModelBll user)
    {
        var userPassword = await _usersRepository.GetUserByIdAsync(id);
        if (userPassword == null)
            throw new EntityNotFoundException($"User with id{id} was not found.");

        await _usersRepository.UpdatePasswordByUserIdAsync(userPassword, user.Password);
    }

    public async Task<List<UserModelBll>> GetAllUsersAsync()
    {
        var users = await _usersRepository.GetAllUsersAsync();
        if (users == null)
            throw new EntityNotFoundException($"Users was not found.");
        var result = _mapper.Map<List<UserModelBll>>(users);
        return result;
    }

    public async Task<UserModelBll> GetUserWithCoursesAndGradesAsync(Guid userId)
    {
        var user = await _usersRepository.GetUserWithCoursesAndGradesAsync(userId);
        if (user == null)
            throw new EntityNotFoundException($"User with id{userId} was not found.");
        var result = _mapper.Map<UserModelBll>(user);
        return result;
    }

    public async Task DeactivateUser(Guid id)
    {
        var user = await _usersRepository.GetUserByIdAsync(id);
        if (user == null)
            throw new EntityNotFoundException($"User with id{id} was not found.");

        await _usersRepository.DeactivateUserAsync(user);
    }

    public async Task DeleteUserAsync(Guid id)
    {
        var user = await _usersRepository.GetUserByIdAsync(id);
        if (user == null)
            throw new EntityNotFoundException($"User with id{id} was not found.");

        await _usersRepository.DeleteUserAsync(user);
    }
}
