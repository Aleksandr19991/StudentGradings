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
    // POST api/users
    [HttpPost, AllowAnonymous]
    //[CustomAuthorize([UserRole.Administrator])]
    public async Task<ActionResult<Guid>> RegisterUserAsync([FromBody] RegisterUserRequest request)
    {
        var userId = mapper.Map<UserModelBll>(request);
        var addedUserId = await usersService.AddUserAsync(userId);
        return Ok(addedUserId);
    }

    //"api/users/login"
    [HttpPost("login"), AllowAnonymous]
    //[CustomAuthorize([UserRole.Teacher, UserRole.Student, UserRole.Administrator])]
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

    //GET api/users/5
    [HttpGet("{id}")]
    //[CustomAuthorize([UserRole.Teacher, UserRole.Student])]
    public async Task<ActionResult<List<UserWithCoursesAndGradesResponse>>> GetUserWithCoursesAndGradesAsync([FromBody] Guid id)
    {
        var courses = await usersService.GetUserWithCoursesAndGradesAsync(id);
        var coursesUser = mapper.Map<List<CourseWithUsersAndGradesResponse>>(courses);
        return Ok(coursesUser);

        //var teacherId = Guid.NewGuid();
        //var studentId = Guid.NewGuid();

    }

    // PUT api/users/5
    [HttpPut("{id}/user")]
    //[Authorize]
    public async Task<IActionResult> UpdateUserAsync([FromRoute] Guid id, [FromBody] UpdateUserRequest request)
    {
        var userId = Guid.NewGuid();
        if (id != userId)
            return Forbid();

        var user = mapper.Map<UserModelBll>(request);
        await usersService.UpdateUserAsync(id, user);
        return NoContent();
    }

    // DELETE api/users/5
    [HttpDelete("{id}")]
    //[CustomAuthorize([UserRole.Administrator])]
    public async Task<IActionResult> DeleteUserAsync([FromRoute] Guid id)
    {
        await usersService.DeleteUserAsync(id);
        return NoContent();
    }

    // PATCH api/users/5
    [HttpPatch("{id}/password")]
    //[CustomAuthorize([UserRole.Teacher, UserRole.Student, UserRole.Administrator])]
    public async Task<IActionResult> UpdatePasswordByUserIdAsync([FromRoute] Guid id, [FromBody] UpdatePasswordByUserRequest request)
    {
        var password = mapper.Map<UserModelBll>(request.Password);
        await usersService.UpdatePasswordByUserIdAsync(id, password);
        return NoContent();
    }
}
