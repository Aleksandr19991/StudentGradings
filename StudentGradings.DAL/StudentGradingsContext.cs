using Microsoft.EntityFrameworkCore;
using StudentGradings.DAL.Configuration;
using StudentGradings.DAL.Models.Dtos;

namespace StudentGradings.DAL;

public class StudentGradingsContext : DbContext
{
    public DbSet<UserDto> Users { get; set; }
    public DbSet<CourseDto> Courses { get; set; }
    public DbSet<UserCourseDto> UserCourses { get; set; }

    public StudentGradingsContext(DbContextOptions<StudentGradingsContext> opts) : base(opts)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.AddCourseEntityConfiguration();
        modelBuilder.AddUserEntityConfiguration();
        modelBuilder.AddUserCourseEntityConfiguration();
    }
}