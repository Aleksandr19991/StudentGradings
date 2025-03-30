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
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, "Teacher"),
                new Claim(ClaimTypes.Role, "Student"),
                new Claim(ClaimTypes.Role, "Administrator")
            };

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConstantAuth.Key));
            var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokeOptions = new JwtSecurityToken(
            issuer: ConstantAuth.Issuer,
            audience: ConstantAuth.Audience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: signinCredentials
            );
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
            return tokenString;
        }
        else
            throw new EntityNotFoundException($"The Email or Password is entered incorrectly, please try again");
    }

    public async Task<Guid> AddUserAsync(UserModel userModel)
    {
        var newUser = _mapper.Map<UserDto>(userModel);
        if (newUser == null)
            throw new EntityNotFoundException($"User with id{userModel} was not found.");

        return await _usersRepository.AddUserAsync(newUser);
    }

    public async Task UpdateUserAsync(Guid id, UserModel user)
    {
        var existingUser = await _usersRepository.GetUserByIdAsync(id);
        if (existingUser == null)
            throw new EntityNotFoundException($"User with id{id} was not found.");

        var newUser = _mapper.Map<UserDto>(user);
        if (newUser == null)
            throw new EntityNotFoundException($"newUser with id{id} was not found.");

        await _usersRepository.UpdateUserAsync(existingUser, newUser);
    }

    public async Task UpdatePasswordByUserIdAsync(Guid id, string newPassword)
    {
        var user = await _usersRepository.GetUserByIdAsync(id);
        if (user == null)
            throw new EntityNotFoundException($"User with id{id} was not found.");

        await _usersRepository.UpdatePasswordByUserIdAsync(user, newPassword);
    }

    public async Task<List<UserModel>> GetAllUsersAsync()
    {
        var users = await _usersRepository.GetAllUsersAsync();
        return users == null ? new List<UserModel>() : _mapper.Map<List<UserModel>>(users);
    }

    public async Task<UserModel> GetUserWithCoursesAndGradesAsync(Guid userId)
    {
        var user = await _usersRepository.GetUserWithCoursesAndGradesAsync(userId);
        if (user == null)
            throw new EntityNotFoundException($"User with id{userId} was not found.");

        return _mapper.Map<UserModel>(user);
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