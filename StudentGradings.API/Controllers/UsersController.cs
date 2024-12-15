using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentGradings.API.Configuration;
using StudentGradings.API.Models;
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
    [HttpPost]
    [CustomAuthorize([UserRole.Administrator])]
    public ActionResult<Guid> RegisterUser([FromBody] RegisterUserRequest request)
    {
        var userId = mapper.Map<UserModelBll>(request);
        var addedUserId = usersService.AddUser(userId);
        return Ok(addedUserId);
    }

    //"api/users/login"
    [HttpPost("login")]
    [CustomAuthorize([UserRole.Teacher, UserRole.Student, UserRole.Administrator])]
    public ActionResult<string> LogIn([FromBody] LoginRequest request)
    {
        if (request is null)
        {
            return BadRequest("Invalid client request");
        }
        var token = usersService.Authenticate(request.Email, request.Password);
        if (token != null)
        {
            return Ok(token);
        }
        else
        {
            return Unauthorized();
        }
    }

    // GET api/users/5
    [HttpGet("{id}/courses")]
    [CustomAuthorize([UserRole.Teacher, UserRole.Student])]
    public ActionResult<List<UserWithCoursesResponse>> GetCoursesByUserId([FromBody] Guid id)
    {
        var courses = usersService.GetCoursesByUserId(id);
        var coursesUser = mapper.Map<List<CourseWithUsersResponse>>(courses);
        return Ok(coursesUser);

        //var teacherId = Guid.NewGuid();
        //var studentId = Guid.NewGuid();

    }

    // PUT api/users/5
    [HttpPut("{id}/user")]
    [Authorize]
    public IActionResult UpdateUser([FromRoute] Guid id, [FromBody] UpdateUserRequest request)
    {
        var userId = Guid.NewGuid();
        if (id != userId)
            return Forbid();

        var user = mapper.Map<UserModelBll>(request);
        usersService.UpdateUser(id, user);
        return NoContent();
    }

    // DELETE api/users/5
    [HttpDelete("{id}")]
    [CustomAuthorize([UserRole.Administrator])]
    public IActionResult DeleteUser([FromRoute] Guid id)
    {
        usersService.DeleteUser(id);
        return NoContent();
    }

    // PATCH api/users/5
    [HttpPatch("{id}/password")]
    [CustomAuthorize([UserRole.Teacher, UserRole.Student, UserRole.Administrator])]
    public IActionResult UpdatePasswordByUserId([FromRoute] Guid id, [FromBody] UpdatePasswordByUserRequest request)
    {
        var password = mapper.Map<UserModelBll>(request.Password);
        usersService.UpdatePasswordByUserId(id, password);
        return NoContent();
    }
}
