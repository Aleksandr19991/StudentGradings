using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudentGradings.API.Models.Requests;
using StudentGradings.API.Models.Responses;
using StudentGradings.BLL.Interfaces;
using StudentGradings.BLL.Models;

namespace StudentGradings.API.Controllers;

[ApiController]
[Route("api/courses")]
public class CoursesController(
    ICoursesService coursesService,
    IGradeBooksService gradeBooksService,
    IMapper mapper
) : ControllerBase
{
    // POST api/<CoursesController>
    [HttpPost]
    public ActionResult<Guid> CreateCourse([FromBody] CreateCourseRequest request)
    {
        var courseId = mapper.Map<CourseModelBll>(request);
        var addedCourseId = coursesService.AddCourse(courseId);
        return Ok(addedCourseId);
    }

    //POST api/<CoursesController>
    [HttpPost("{id}/grade")]
    public IActionResult AddGradeByCourseId([FromRoute] Guid id, [FromBody] AddGradeRequest request)
    {
        gradeBooksService.AddGradeByCourseId(id, request.User.Id);
        return NoContent();
    }

    // GET api/<CoursesController>
    [HttpGet("{id}/users")]
    public ActionResult<List<CourseWithUsersResponse>> GetUsersByCoursesId([FromRoute] Guid id)
    {
        var users = coursesService.GetUsersByCourseId(id);
        var students = mapper.Map<List<CourseWithUsersResponse>>(users);
        return Ok(students);
    }

    // GET api/<CoursesController>
    [HttpGet("{id}/grades")]
    public ActionResult<List<GradeBookResponse>> GetGradesByCoursesId([FromRoute] Guid id)
    {
        var grades = gradeBooksService.GetGradesByCourseId(id);
        var gradeCourse = mapper.Map<List<GradeBookResponse>>(grades);
        return Ok(gradeCourse);
    }

    // PATCH api/<UsersController>
    [HttpPatch("{id}/deactivate")]
    public IActionResult DeactivateCourse([FromRoute] Guid id)
    {
        coursesService.DeactivateCourse(id);
        return NoContent();
    }

    // DELETE api/users/5
    [HttpDelete("{id}")]
    public IActionResult DeleteUser([FromRoute] Guid id)
    {
        coursesService.DeleteCourse(id);
        return NoContent();
    }
}
