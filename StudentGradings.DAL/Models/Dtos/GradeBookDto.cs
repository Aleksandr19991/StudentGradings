﻿namespace StudentGradings.DAL.Models.Dtos;

public class GradeBookDto
{
    public Guid Id { get; set; }
    public float Grade { get; set; }
    public bool IsDeactevated { get; set; }
    public UserDto User { get; set; }
    public CourseDto Course { get; set; }
}
