using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudentGradings.API.Configuration;
using StudentGradings.API.Models;
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
    [CustomAuthorize([UserRole.Administrator])]
    public ActionResult<Guid> CreateCourse([FromBody] CreateCourseRequest request)
    {
        var courseId = mapper.Map<CourseModelBll>(request);
        var addedCourseId = coursesService.AddCourse(courseId);
        return Ok(addedCourseId);
    }

    //POST api/<CoursesController>
    [HttpPost("{id}/grade")]
    [CustomAuthorize([UserRole.Teacher])]
    public IActionResult AddGradeByCourseId([FromRoute] Guid id, [FromBody] AddGradeRequest request)
    {
        gradeBooksService.AddGradeByCourseId(id, request.User.Id);
        return NoContent();
    }

    // GET api/<CoursesController>
    [HttpGet("{id}/users")]
    [CustomAuthorize([UserRole.Teacher])]
    public ActionResult<List<CourseWithUsersResponse>> GetUsersByCoursesId([FromRoute] Guid id)
    {
        var users = coursesService.GetUsersByCourseId(id);
        var students = mapper.Map<List<CourseWithUsersResponse>>(users);
        return Ok(students);
    }

    // GET api/<CoursesController>
    [HttpGet("{id}/grades")]
    [CustomAuthorize([UserRole.Teacher, UserRole.Student])]
    public ActionResult<List<GradeBookResponse>> GetGradesByCoursesId([FromRoute] Guid id)
    {
        var grades = gradeBooksService.GetGradesByCourseId(id);
        var gradeCourse = mapper.Map<List<GradeBookResponse>>(grades);
        return Ok(gradeCourse);
    }

    // PUT api/courses/5
    [HttpPut("{id}/course")]
    [CustomAuthorize([UserRole.Administrator])]
    public IActionResult UpdateCourse([FromRoute] Guid id, [FromBody] UpdateCourseRequest request)
    {
        var course = mapper.Map<CourseModelBll>(request);
        coursesService.UpdateCourse(id, course);
        return NoContent();
    }

    // DELETE api/users/5
    [HttpDelete("{id}")]
    [CustomAuthorize([UserRole.Administrator])]
    public IActionResult DeleteCourse([FromRoute] Guid id)
    {
        coursesService.DeleteCourse(id);
        return NoContent();
    }

    // PATCH api/<UsersController>
    [HttpPatch("{id}/grade")]
    [CustomAuthorize([UserRole.Teacher])]
    public IActionResult UpdateGradeByCourseId([FromRoute] Guid id, [FromBody] UpdateGradeByCourseRequest request)
    {
        var grade = mapper.Map<GradeBookModelBll>(request);
        gradeBooksService.UpdateGradeByCourseId(id, grade);
        return NoContent();
    }

    // PATCH api/<UsersController>
    [HttpPatch("{id}/deactivate")]
    [CustomAuthorize([UserRole.Administrator])]
    public IActionResult DeactivateCourse([FromRoute] Guid id)
    {
        coursesService.DeactivateCourse(id);
        return NoContent();
    }
}
