using Microsoft.AspNetCore.Mvc;
using StudentGradings.API.Models.Responses;

namespace StudentGradings.API.Controllers;

[ApiController]
[Route("api/courses")]
public class CoursesController : ControllerBase
{
    // POST api/<CoursesController>
    [HttpPost]
    public ActionResult<Guid> CreateCourse([FromBody] string value)
    {
        var addedCourseId = Guid.NewGuid();
        return Ok(addedCourseId);
    }

    // GET: api/<CoursesController>
    [HttpGet]
    public ActionResult<List<CourseResponse>> GetCourses()
    {
        var course = new List<CourseResponse>();
        return course.ToList();
    }

    // GET api/<CoursesController>
    [HttpGet("api/courses/:id/student")]
    public ActionResult<List<StudentResponse>> GetStudentsByCoursesId([FromRoute] Guid id)
    {
        var student = new List<StudentResponse>();
        return student.ToList();
    }

    // GET api/<CoursesController>
    [HttpGet("api/courses/:id/grade")]
    public ActionResult<List<CourseResponse>> GetGradesByCoursesId([FromRoute] Guid id)
    {
        var grade = new CourseResponse();
        return Ok(grade);
    }

    // PATCH api/<UsersController>
    [HttpPatch("{id}/deactivate")]
    public IActionResult DeactivateCourse([FromRoute] Guid id)
    {
        return NoContent();
    }
}
