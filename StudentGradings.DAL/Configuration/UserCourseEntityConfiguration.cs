using Microsoft.EntityFrameworkCore;
using StudentGradings.DAL.Models.Dtos;

namespace StudentGradings.DAL.Configuration;

internal static class UserCourseEntityConfiguration
{
    internal static void AddUserCourseEntityConfiguration(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserCourseDto>()
            .ToTable("UserCourses")
            .HasKey(c => c.Id);

        modelBuilder.Entity<UserCourseDto>()
            .HasOne(gb => gb.User)
            .WithMany(u => u.UserCourses)
            .HasForeignKey(gb => gb.UserId);

        modelBuilder.Entity<UserCourseDto>()
            .HasOne(gb => gb.Course)
            .WithMany(c => c.UserCourses)
            .HasForeignKey(gb => gb.CourseId);
    }
}
