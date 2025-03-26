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
    //[CustomAuthorize([UserRole.Administrator])]
    public async Task<ActionResult<Guid>> CreateCourseAsync([FromBody] CreateCourseRequest request)
    {
        var courseId = mapper.Map<CourseModelBll>(request);
        var addedCourseId = await coursesService.AddCourseAsync(courseId);
        return Ok(addedCourseId);
    }

    // POST api/<CoursesController>
    [HttpPost("gradeBook")]
    //[CustomAuthorize([UserRole.Administrator])]
    public async Task<ActionResult<Guid>> CreateGradeBookAsync([FromBody] CreateGradeBookRequest request)
    {
        var gradeBookId = mapper.Map<GradeBookModelBll>(request);
        var addedGradeBookId = await gradeBooksService.AddGradeBookAsync(gradeBookId);
        return Ok(addedGradeBookId);
    }

    //POST api/<CoursesController>
    [HttpPost("grade")]
    //[CustomAuthorize([UserRole.Teacher])]
    public async Task<IActionResult> AddGradeByCourseIdAsync([FromRoute] Guid id, [FromBody] AddGradeRequest request)
    {
        await gradeBooksService.AddGradeByCourseIdAsync(id, request.User.Id);
        return NoContent();
    }

    //GET api/<CoursesController>
    [HttpGet("{id}/users")]
    //[CustomAuthorize([UserRole.Teacher])]
    public async Task<ActionResult<List<CourseWithUsersAndGradesResponse>>> GetCourseWithUsersAndGradesAsync([FromRoute] Guid id)
    {
        var users = await coursesService.GetCourseWithUsersAndGradesAsync(id);
        var students = mapper.Map<List<CourseWithUsersAndGradesResponse>>(users);
        return Ok(students);
    }

    //GET api/<CoursesController>
    [HttpGet("{id}/grades")]
    //[CustomAuthorize([UserRole.Teacher, UserRole.Student])]
    public async Task<ActionResult<List<GradeBookResponse>>> GetGradesByCoursesIdAsync([FromRoute] Guid id)
    {
        var grades = await gradeBooksService.GetGradesByCourseIdAsync(id);
        var gradeCourse = mapper.Map<List<GradeBookResponse>>(grades);
        return Ok(gradeCourse);
    }

    // PUT api/courses/5
    [HttpPut("{id}/course")]
    //[CustomAuthorize([UserRole.Administrator])]
    public async Task<IActionResult> UpdateCourseAsync([FromRoute] Guid id, [FromBody] UpdateCourseRequest request)
    {
        var course = mapper.Map<CourseModelBll>(request);
        await coursesService.UpdateCourseAsync(id, course);
        return NoContent();
    }

    // DELETE api/users/5
    [HttpDelete("{id}")]
    //[CustomAuthorize([UserRole.Administrator])]
    public async Task<IActionResult> DeleteCourseAsync([FromRoute] Guid id)
    {
        await coursesService.DeleteCourseAsync(id);
        return NoContent();
    }

    // PATCH api/<UsersController>
    [HttpPatch("{id}/grade")]
    //[CustomAuthorize([UserRole.Teacher])]
    public async Task<IActionResult> UpdateGradeByCourseIdAsync([FromRoute] Guid id, [FromBody] UpdateGradeByCourseRequest request)
    {
        var grade = mapper.Map<GradeBookModelBll>(request);
        await gradeBooksService.UpdateGradeByCourseIdAsync(id, grade);
        return NoContent();
    }

    // PATCH api/<UsersController>
    [HttpPatch("{id}/deactivate")]
    //[CustomAuthorize([UserRole.Administrator])]
    public async Task<IActionResult> DeactivateCourseAsync([FromRoute] Guid id)
    {
        await coursesService.DeactivateCourseAsync(id);
        return NoContent();
    }
}
