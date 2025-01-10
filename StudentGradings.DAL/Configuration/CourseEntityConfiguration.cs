using Microsoft.EntityFrameworkCore;
using StudentGradings.DAL.Models.Dtos;

namespace StudentGradings.DAL.Configuration;

internal static class CourseEntityConfiguration
{
    internal static void AddCourseEntityConfiguration(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CourseDto>()
            .ToTable("Courses")
            .HasKey(c => c.Id);

        modelBuilder.Entity<CourseDto>()
            .Property(c => c.Name)
            .HasMaxLength(30);

        modelBuilder.Entity<CourseDto>()
            .Property(c => c.Description)
            .HasMaxLength(90);

        modelBuilder.Entity<CourseDto>()
            .Property(c => c.Hours)
            .IsRequired();
    }
}
