using Microsoft.EntityFrameworkCore;
using StudentGradings.DAL.Models.Dtos;

namespace StudentGradings.DAL.Configuration;

internal static class GradeBookEntityConfiguration
{
    internal static void AddGradeBookEntityConfiguration(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<GradeBookDto>()
            .ToTable("GradeBooks")
            .HasKey(c => new { c.UserId, c.CourseId });

        modelBuilder.Entity<GradeBookDto>()
            .HasOne(gb => gb.User)
            .WithMany(u => u.GradeBooks)
            .HasForeignKey(gb => gb.UserId);

        modelBuilder.Entity<GradeBookDto>()
            .HasOne(gb => gb.Course)
            .WithMany(c => c.GradeBooks)
            .HasForeignKey(gb => gb.CourseId);
    }
}
