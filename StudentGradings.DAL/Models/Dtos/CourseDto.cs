﻿namespace StudentGradings.DAL.Models.Dtos;

public class CourseDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Hours { get; set; }
    public bool IsDeactivated { get; set; }
    public ICollection<GradeBookDto> GradeBooks { get; set; } = [];
}
