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
    IUserCoursesService userCoursesService,
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
    // POST: api/UserCourses/{courseId}/users/{userId}/grade
    [HttpPost("{courseId}/users/{userId}/grade")]
    public async Task<IActionResult> AddGrade(Guid userId, Guid courseId, [FromBody] GradeRequest request)
    {
        await userCoursesService.AddGradeByUserIdAndCourseIdAsync(userId, courseId, request.Grade);
        return Ok();
    }

    // GET: api/courses
    [HttpGet]
    public async Task<ActionResult<List<CourseModelShort>>> GetAllCourses()
    {
        var courses = await coursesService.GetAllCoursesAsync();
        return Ok(courses);
    }

    ////[CustomAuthorize([UserRole.Teacher])]
    //GET /api/usercourses/users/{userId}/courses/{courseId}/grades
    [HttpGet("users/{userId}/courses/{courseId}/grades")]
    public async Task<IActionResult> GetGradesByUserAndCourse(Guid userId, Guid courseId)
    {
        var grades = await userCoursesService.GetGradesByCourseIdAsync(userId, courseId);
        var response = mapper.Map<List<UserCourseResponse>>(grades);
        return Ok(response);
    }

    // GET: api/usercourses/grades/{userId}
    [HttpGet("grades/{userId}")]
    public async Task<IActionResult> GetAllGradesByUserId(Guid userId)
    {
        var grades = await userCoursesService.GetAllGradesByUserIdAsync(userId);
        return Ok(grades);
    }

    // PUT api/courses/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCourseAsync([FromRoute] Guid id, [FromBody] UpdateCourseRequest request)
    {
        var course = mapper.Map<CourseModel>(request);
        await coursesService.UpdateCourseAsync(id, course);
        return NoContent();
    }

    //[CustomAuthorize([UserRole.Teacher])]
    // PUT api/usercourses/{userId}/{courseId}/grade
    [HttpPut("{userId:guid}/{courseId:guid}/grade")]
    public async Task<IActionResult> UpdateGrade(Guid userId, Guid courseId, [FromBody] UpdateGradeRequest request)
    {
        await userCoursesService.UpdateGradeByCourseIdAndUserIdAsync(userId, courseId, request.Grade);
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

    // DELETE api/grades/{userId}/{courseId}
    [HttpDelete("{userId:guid}/{courseId:guid}")]
    public async Task<IActionResult> DeleteGrade(Guid userId, Guid courseId)
    {
        await userCoursesService.DeleteGradeByCourseIdAndUserIdAsync(userId, courseId);
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
