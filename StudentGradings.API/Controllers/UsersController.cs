using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentGradings.API.Configuration;
using StudentGradings.API.Models.Requests;
using StudentGradings.API.Models.Responses;
using StudentGradings.BLL.Interfaces;
using StudentGradings.BLL.Models;
using StudentGradings.CORE;

namespace StudentGradings.API.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController(IUsersService usersService, IMapper mapper) : ControllerBase
{
    // POST api/users
    [HttpPost, AllowAnonymous]
    public async Task<ActionResult<Guid>> RegisterUserAsync([FromBody] RegisterUserRequest request)
    {
        var userModel = mapper.Map<UserModel>(request);
        var addedUserId = await usersService.AddUserAsync(userModel);
        return Ok(addedUserId);
    }

    //"api/users/login"
    [HttpPost("login"), AllowAnonymous]
    public async Task<ActionResult<string>> LogInAsync([FromBody] LoginRequest request)
    {
        if (request is null)
        {
            return BadRequest("Invalid client request");
        }
        var token = await usersService.AuthenticateAsync(request.Email, request.Password);
        if (token != null)
        {
            return Ok(token);
        }
        else
        {
            return Unauthorized();
        }
    }

    // GET: api/users
    [HttpGet, AllowAnonymous]
    public async Task<ActionResult<List<UserModel>>> GetAllUsers()
    {
        var users = await usersService.GetAllUsersAsync();
        return Ok(users);
    }

    // GET api/users/{id}
    [HttpGet("{id}"), AllowAnonymous]
    public async Task<ActionResult<UserResponse>> GetUserWithCoursesAndGrades(Guid id)
    {
        var user = await usersService.GetUserWithCoursesAndGradesAsync(id);
        var response = mapper.Map<UserResponse>(user);
        return Ok(response);
    }

    // PUT api/users/{id}
    [HttpPut("{id}")]
    [CustomAuthorize([UserRole.Administrator, UserRole.Student])]
    public async Task<IActionResult> UpdateUserAsync([FromRoute] Guid id, [FromBody] UpdateUserRequest request)
    {
        var user = await usersService.GetUserByIdAsync(id);
        var updateUser = mapper.Map<UserModel>(request);
        await usersService.UpdateUserAsync(id, updateUser);
        return NoContent();
    }

    // DELETE api/users/5
    [HttpDelete("{id}")]
    [CustomAuthorize([UserRole.Administrator])]
    public async Task<IActionResult> DeleteUserAsync([FromRoute] Guid id)
    {
        await usersService.DeleteUserAsync(id);
        return NoContent();
    }

    //[CustomAuthorize([UserRole.Teacher, UserRole.Student, UserRole.Administrator])]
    // PATCH api/users/5
    [HttpPatch("{id}/password")]
    public async Task<IActionResult> UpdatePasswordByUserIdAsync([FromRoute] Guid id, [FromBody] UpdatePasswordByUserRequest request)
    {
        await usersService.UpdatePasswordAsync(id, request.NewPassword);
        return NoContent();
    }
}
