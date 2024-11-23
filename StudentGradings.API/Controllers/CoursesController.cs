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

    // GET api/<CoursesController>
    [HttpGet("api/courses/id/student")]
    public ActionResult<List<CourseWithStudents>> GetStudentsByCoursesId([FromRoute] Guid id)
    {
        var student = new List<CourseWithStudents>();
        return student.ToList();
    }

    // GET api/<CoursesController>
    [HttpGet("api/courses/id/grade")]
    public ActionResult<List<StudentResponse>> GetGradesByCoursesId([FromRoute] Guid id)
    {
        var gradeCourse = new StudentResponse();
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
