using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudentGradings.API.Models.Requests;
using StudentGradings.API.Models.Responses;
using StudentGradings.BLL;
using StudentGradings.BLL.Interfaces;
using StudentGradings.BLL.Models;

namespace StudentGradings.API.Controllers;

[ApiController]
[Route("api/users")]

public class UsersController(IUsersService usersService, IMapper mapper) : ControllerBase
{
    // POST api/users
    [HttpPost]
    public ActionResult<Guid> RegisterUser([FromBody] RegisterUserRequest request)
    {
        var userId = mapper.Map<UserModelBll>(request);
        var addedUserId = usersService.AddUser(userId);
        return Ok(addedUserId);
    }

    //"api/users/login"
    [HttpPost("login")]
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
    public ActionResult<List<UserWithCoursesResponse>> GetCoursesByUserId([FromRoute] Guid id)
    {
        var courses = usersService.GetCoursesByUserId(id);
        var coursesUser = mapper.Map<List<CourseWithUsersResponse>>(courses);
        return Ok(coursesUser);
    }

    // PATCH api/users/5
    [HttpPatch("{id}/password")]
    public IActionResult UpdatePasswordByUserId([FromRoute] Guid id, [FromBody] UpdatePasswordByUserRequest request)
    {
        var password = mapper.Map<UserModelBll>(request.Password);
        usersService.UpdatePasswordByUserId(id, password);
        return NoContent();
    }

    // PATCH api/users/5
    [HttpPatch("{id}/deactivate")]
    public IActionResult DeactivateUser([FromRoute] Guid id)
    {
        usersService.DeactivateUser(id);
        return NoContent();
    }

    // DELETE api/users/5
    [HttpDelete("{id}")]
    public IActionResult DeleteUser([FromRoute] Guid id)
    {
        usersService.DeleteUser(id);
        return NoContent();
    }
}
