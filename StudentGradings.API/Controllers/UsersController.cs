using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentGradings.API.Models.Requests;
using StudentGradings.API.Models.Responses;
using StudentGradings.BLL.Interfaces;
using StudentGradings.BLL.Models;

namespace StudentGradings.API.Controllers;

[ApiController]
[Route("api/users")]

public class UsersController(IUsersService usersService, IMapper mapper) : ControllerBase
{
    ////[CustomAuthorize([UserRole.Administrator])]
    // POST api/users
    [HttpPost, AllowAnonymous]
    public async Task<ActionResult<Guid>> RegisterUserAsync([FromBody] RegisterUserRequest request)
    {
        var userModel = mapper.Map<UserModel>(request);
        var addedUserId = await usersService.AddUserAsync(userModel);
        return Ok(addedUserId);
    }

    //[CustomAuthorize([UserRole.Teacher, UserRole.Student, UserRole.Administrator])]
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

    //[CustomAuthorize([UserRole.Teacher, UserRole.Student])]
    //GET api/users/5
    [HttpGet("{id}")]
    public async Task<ActionResult<UserWithCoursesAndGradesResponse>> GetUserWithCoursesAndGradesAsync([FromRoute] Guid id)
    {
        var user = await usersService.GetUserWithCoursesAndGradesAsync(id);
        var userGrade = mapper.Map<UserWithCoursesAndGradesResponse>(user);
        return Ok(userGrade);

        //var teacherId = Guid.NewGuid();
        //var studentId = Guid.NewGuid();
    }

    //[Authorize]
    // PUT api/users/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUserAsync([FromRoute] Guid id, [FromBody] UpdateUserRequest request)
    {
        var user = await usersService.GetUserByIdAsync(id);
        //if (user == null || usersService.IsUserAuthorizedAsync(id))
        //    return Forbid();

        var updateUser = mapper.Map<UserModel>(request);
        await usersService.UpdateUserAsync(id, updateUser);
        return NoContent();
    }

    //[CustomAuthorize([UserRole.Administrator])]
    // DELETE api/users/5
    [HttpDelete("{id}")]
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
