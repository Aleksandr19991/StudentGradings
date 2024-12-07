﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudentGradings.API.Models.Requests;
using StudentGradings.API.Models.Responses;
using StudentGradings.BLL.Interfaces;

namespace StudentGradings.API.Controllers;

[ApiController]
[Route("api/users")]

public class UsersController(IUsersService userService, IMapper mapper) : ControllerBase
{
    // POST api/users
    [HttpPost]
    public ActionResult<Guid> RegisterUser([FromBody] RegisterUserRequest request)
    {
        var addedUserId = Guid.NewGuid();
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
        var token = userService.Authenticate(request.Email, request.Password);
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
        var course = new List<UserWithCoursesResponse>();
        return course.ToList();
    }

    // PUT api/users/5
    [HttpPut("{id}")]
    public IActionResult UpdateUser([FromRoute] Guid id, [FromBody] UpdateUserRequest request)
    {
        return NoContent();
    }

    // PATCH api/users/5
    [HttpPatch("{id}/deactivate")]
    public IActionResult DeactivateUser([FromRoute] Guid id)
    {
        return NoContent();
    }

    // DELETE api/users/5
    [HttpDelete("{id}")]
    public IActionResult DeleteUser([FromRoute] Guid id)
    {
        return NoContent();
    }
}
