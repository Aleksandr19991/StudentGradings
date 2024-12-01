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
   
    //// GET api/<CoursesController>
    //[HttpGet("api/courses/id/grade")]
    //public ActionResult<List<CourseWithStudents>> GetGradesByAllCoursesId([FromRoute] Guid id)
    //{
    //    var grades = new CourseWithStudents();
    //    return Ok(grades);
    //}

    // PATCH api/<UsersController>
    [HttpPatch("{id}/deactivate")]
    public IActionResult DeactivateCourse([FromRoute] Guid id)
    {
        return NoContent();
    }
}
