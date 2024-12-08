using Microsoft.AspNetCore.Mvc;
using StudentGradings.API.Models.Requests;
using StudentGradings.API.Models.Responses;

namespace StudentGradings.API.Controllers;

[ApiController]
[Route("api/courses")]
public class CoursesController : ControllerBase
{
    // POST api/<CoursesController>
    [HttpPost]
    public ActionResult<Guid> CreateCourse(CreateCourseRequest request)
    {
        var addedCourseId = Guid.NewGuid();
        return Ok(addedCourseId);
    }

    //POST api/<CoursesController>
    [HttpPost]
    public ActionResult<Guid> AddGradeByCourseId(AddGradeRequest request)
    {
        var addedGrade = Guid.NewGuid();
        return Ok(addedGrade);
    }

    // GET api/<CoursesController>
    [HttpGet("{id}/users")]
    public ActionResult<List<CourseWithUsersResponse>> GetUsersByCoursesId([FromRoute] Guid id)
    {
        var student = new List<CourseWithUsersResponse>();
        return student.ToList();
    }

    // GET api/<CoursesController>
    [HttpGet("{id}/grades")]
    public ActionResult<List<GradeBookResponse>> GetGradesByCoursesId([FromRoute] Guid id)
    {
        var gradeCourse = new GradeBookResponse();
        return Ok(gradeCourse);
    }

    // PATCH api/<UsersController>
    [HttpPatch("{id}/deactivate")]
    public IActionResult DeactivateCourse([FromRoute] Guid id)
    {
        return NoContent();
    }
}
