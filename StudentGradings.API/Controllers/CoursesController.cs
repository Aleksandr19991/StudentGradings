using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StudentGradings.API.Configuration;
using StudentGradings.API.Models.Requests;
using StudentGradings.API.Models.Responses;
using StudentGradings.BLL.Interfaces;
using StudentGradings.BLL.Models;
using StudentGradings.CORE;

namespace StudentGradings.API.Controllers;

[ApiController]
[Route("api/courses")]
public class CoursesController(
    ICoursesService coursesService,
    IUserCoursesService userCoursesService,
    IMapper mapper
) : ControllerBase
{
    // POST api/<CoursesController>
    [HttpPost]
    [CustomAuthorize([UserRole.Administrator, UserRole.Teacher])]
    public async Task<ActionResult<Guid>> CreateCourseAsync([FromBody] CreateCourseRequest request)
    {
        var courseModel = mapper.Map<CourseModel>(request);
        var addedCourseId = await coursesService.AddCourseAsync(courseModel);
        return Ok(addedCourseId);
    }

    // POST api/UserCourses/{courseId}/users/{userId}/grade
    [HttpPost("{courseId}/users/{userId}/grade")]
    [CustomAuthorize([UserRole.Teacher])]
    public async Task<IActionResult> AddGrade(Guid userId, Guid courseId, [FromBody] GradeRequest request)
    {
        await userCoursesService.AddGradeByUserIdAndCourseIdAsync(userId, courseId, request.Grade);
        return Ok();
    }

    // GET api/courses
    [HttpGet, AllowAnonymous]
    public async Task<ActionResult<List<CourseModelShort>>> GetAllCourses()
    {
        var courses = await coursesService.GetAllCoursesAsync();
        return Ok(courses);
    }

    // GET /api/usercourses/users/{userId}/courses/{courseId}/grades
    [HttpGet("users/{userId}/courses/{courseId}/grades"), AllowAnonymous]
    public async Task<IActionResult> GetGradesByUserAndCourse(Guid userId, Guid courseId)
    {
        var grades = await userCoursesService.GetGradesByCourseIdAsync(userId, courseId);
        var response = mapper.Map<List<UserCourseResponse>>(grades);
        return Ok(response);
    }

    // GET api/usercourses/grades/{userId}
    [HttpGet("grades/{userId}"), AllowAnonymous]
    public async Task<IActionResult> GetAllGradesByUserId(Guid userId)
    {
        var grades = await userCoursesService.GetAllGradesByUserIdAsync(userId);
        return Ok(grades);
    }

    // PUT api/courses/{id}
    [HttpPut("{id}")]
    [CustomAuthorize([UserRole.Administrator, UserRole.Teacher])]
    public async Task<IActionResult> UpdateCourseAsync([FromRoute] Guid id, [FromBody] UpdateCourseRequest request)
    {
        var course = mapper.Map<CourseModel>(request);
        await coursesService.UpdateCourseAsync(id, course);
        return NoContent();
    }

    // PUT api/usercourses/{userId}/{courseId}/grade
    [HttpPut("{userId:guid}/{courseId:guid}/grade")]
    [CustomAuthorize([UserRole.Teacher])]
    public async Task<IActionResult> UpdateGrade(Guid userId, Guid courseId, [FromBody] UpdateGradeRequest request)
    {
        await userCoursesService.UpdateGradeByCourseIdAndUserIdAsync(userId, courseId, request.Grade);
        return NoContent();
    }

    // DELETE api/courses/{id}
    [HttpDelete("{id}")]
    [CustomAuthorize([UserRole.Administrator])]
    public async Task<IActionResult> DeleteCourseAsync([FromRoute] Guid id)
    {
        await coursesService.DeleteCourseAsync(id);
        return NoContent();
    }

    // DELETE api/grades/{userId}/{courseId}
    [HttpDelete("{userId:guid}/{courseId:guid}")]
    [CustomAuthorize([UserRole.Teacher])]
    public async Task<IActionResult> DeleteGrade(Guid userId, Guid courseId)
    {
        await userCoursesService.DeleteGradeByCourseIdAndUserIdAsync(userId, courseId);
        return NoContent();
    }

    // PATCH api/<CoursesController>
    [HttpPatch("{id}/deactivate")]
    [CustomAuthorize([UserRole.Administrator])]
    public async Task<IActionResult> DeactivateCourseAsync([FromRoute] Guid id)
    {
        await coursesService.DeactivateCourseAsync(id);
        return NoContent();
    }
}