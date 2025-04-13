using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StudentGradings.API.Models.Requests;
using StudentGradings.API.Models.Responses;
using StudentGradings.BLL.Exeptions;
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
    //[CustomAuthorize([UserRole.Administrator])]
    // POST api/<CoursesController>
    [HttpPost]
    public async Task<ActionResult<Guid>> CreateCourseAsync([FromBody] CreateCourseRequest request)
    {
        var courseModel = mapper.Map<CourseModel>(request);
        var addedCourseId = await coursesService.AddCourseAsync(courseModel);
        return Ok(addedCourseId);
    }

    //[CustomAuthorize([UserRole.Teacher])]
    //POST api/courses/{courseId}/grades
    [HttpPost("courses/{id}/grades")]
    public async Task<IActionResult> AddGradeByCourseIdAsync([FromRoute] Guid courseId, [FromBody] AddGradeRequest request)
    {
        await gradeBooksService.AddGradeByCourseIdAndUserIdAsync(courseId, request.User.Id, request.Grade);
        return Ok("Grade added successfully");
    }

    //[CustomAuthorize([UserRole.Teacher])]
    //GET api/<CoursesController>
    [HttpGet("{id}/courses")]
    public async Task<ActionResult<List<CourseWithUsersAndGradesResponse>>> GetCourseWithUsersAndGradesAsync([FromRoute] Guid id)
    {
        var courses = await coursesService.GetCourseWithUsersAndGradesAsync(id);
        var students = mapper.Map<List<CourseWithUsersAndGradesResponse>>(courses);
        return Ok(students);
    }

    //[CustomAuthorize([UserRole.Teacher, UserRole.Student])]
    //GET api/courses/{id}/grades
    [HttpGet("{id}/grades")]
    public async Task<ActionResult<List<GradeBookResponse>>> GetGradesByCourseIdAsync([FromRoute] Guid id)
    {
        var grades = await gradeBooksService.GetGradesByCourseIdAsync(id);
        var gradeCourse = mapper.Map<List<GradeBookResponse>>(grades);
        return Ok(gradeCourse);
    }

    ////[CustomAuthorize([UserRole.Administrator])]
    // PUT api/courses/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCourseAsync([FromRoute] Guid id, [FromBody] UpdateCourseRequest request)
    {
            var course = mapper.Map<CourseModel>(request);
            await coursesService.UpdateCourseAsync(id, course);
            return NoContent();
    }

    //[CustomAuthorize([UserRole.Administrator])]
    // DELETE api/courses/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCourseAsync([FromRoute] Guid id)
    {
        await coursesService.DeleteCourseAsync(id);
        return NoContent();
    }

    //[CustomAuthorize([UserRole.Teacher])]
    // PATCH api/<CoursesController>
    [HttpPatch("courses/{courseId}/grade")]
    public async Task<IActionResult> UpdateGradeByCourseIdAsync([FromRoute] Guid courseId, [FromBody] UpdateGradeByCourseRequest request)
    {
        await gradeBooksService.UpdateGradeByCourseIdAndUserIdAsync(courseId, request.User.Id, request.Grade);
        return NoContent();
    }

    //[CustomAuthorize([UserRole.Administrator])]
    // PATCH api/<CoursesController>
    [HttpPatch("{id}/deactivate")]
    public async Task<IActionResult> DeactivateCourseAsync([FromRoute] Guid id)
    {
        await coursesService.DeactivateCourseAsync(id);
        return NoContent();
    }
}
