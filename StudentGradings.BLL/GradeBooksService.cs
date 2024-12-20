﻿using AutoMapper;
using StudentGradings.BLL.Exeptions;
using StudentGradings.BLL.Interfaces;
using StudentGradings.BLL.Mappings;
using StudentGradings.BLL.Models;
using StudentGradings.DAL.Interfaces;
using StudentGradings.DAL.Models.Dtos;

namespace StudentGradings.BLL;

public class GradeBooksService : IGradeBooksService
{
    private ICoursesRepository _coursesRepository;
    private IUsersRepository _usersRepository;
    private IGradeBooksRepository _gradeBooksRepository;
    private Mapper _mapper;

    public GradeBooksService(
        IGradeBooksRepository gradeBooksRepository,
        ICoursesRepository coursesRepository,
        IUsersRepository usersRepository
    )
    {
        _coursesRepository = coursesRepository;
        _usersRepository = usersRepository;
        _gradeBooksRepository = gradeBooksRepository;

        var config = new MapperConfiguration(
            cfg =>
            {
                cfg.AddProfile(new CourseMapperProfile());
                cfg.AddProfile(new UserMapperProfile());
                cfg.AddProfile(new GradeBookProfile());
            });
        _mapper = new Mapper(config);
    }

    public Guid AddGradeBook(GradeBookModelBll gradeBookId)
    {
        var newGradeBook = _mapper.Map<GradeBookDto>(gradeBookId);
        if (newGradeBook == null)
            throw new EntityNotFoundException($"Course with id{gradeBookId} was not found.");

        var result = _gradeBooksRepository.AddGradeBook(newGradeBook);
        return result;
    }

    public void AddGradeByCourseId(Guid courseId, Guid userId)
    {
        var gradeBook = _gradeBooksRepository.GetGradeBook(courseId, userId);
        if (gradeBook != null)
            throw new EntityConflictException($"Grade with user id{userId} and course id {userId} already exists.");

        var course = _coursesRepository.GetCourseById(courseId);
        if (course == null)
            throw new EntityNotFoundException($"Course with id{courseId} was not found.");

        if (course.IsDeactivated)
            throw new EntityConflictException($"Course with id{courseId} is deactivated.");

        var user = _usersRepository.GetUserById(userId);
        if (user == null)
            throw new EntityNotFoundException($"User with id{userId} was not found.");

        if (user.IsDeactivated)
            throw new EntityConflictException($"User with id{userId} is deactivated.");

        var newGradeBook = new GradeBookDto()
        {
            Course = course,
            User = user
        };

        _gradeBooksRepository.AddGradeByCourseId(newGradeBook);
    }

    public void UpdateGradeByCourseId(Guid id, GradeBookModelBll gradeBook)
    {
        var gradeCourse = _gradeBooksRepository.GetGradeBook(gradeBook.CourseId, gradeBook.UserId);
        if (gradeCourse == null)
            throw new EntityNotFoundException($"GradeBook with course id{gradeBook.CourseId} and user id{gradeBook.UserId}  was not found.");

        _gradeBooksRepository.UpdateGrade(gradeCourse, gradeBook.Grade);
    }

    public GradeBookModelBll GetGradeBook(Guid courseId, Guid userId)
    {
        var gradebook = _gradeBooksRepository.GetGradeBook(courseId, userId);
        if (gradebook == null)
            throw new EntityNotFoundException($"Gradebook with id{courseId} and with id{userId} was not found.");

        var result = _mapper.Map<GradeBookModelBll>(gradebook);
        return result;
    }


    public GradeBookModelBll GetGradesByCourseId(Guid courseId)
    {
        var grades = _gradeBooksRepository.GetGradesByCourseId(courseId);
        if (grades == null)
            throw new EntityNotFoundException($"Grades with id{courseId} was not found.");

        var result = _mapper.Map<GradeBookModelBll>(grades);
        return result;
    }
}
