using Microsoft.EntityFrameworkCore;
using StudentGradings.DAL.Models.Dtos;

namespace StudentGradings.DAL;

public class Context : DbContext
{
    public DbSet<UserDto> Users { get; set; }
    public DbSet<UserRoleDto> UserRoles { get; set; }
    public DbSet<CourseDto> Courses { get; set; }
    public DbSet<GradeBookDto> GradeBooks { get; set; }
    public Context()
    {
        Database.EnsureCreated();
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(Environment.GetEnvironmentVariable("StudentGrading"));
    }
}