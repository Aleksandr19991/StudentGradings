using Microsoft.EntityFrameworkCore;
using StudentGradings.DAL.Models.Dtos;

namespace StudentGradings.DAL.Configuration
{
    internal static class GradeBookEntityConfiguration
    {
        internal static void AddGradeBookEntityConfiguration(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GradeBookDto>()
                .ToTable("GradeBooks")
                .HasKey(c => c.Id);

            modelBuilder.Entity<GradeBookDto>()
                .HasKey(c => new { c.UserId, c.CourseId });

            modelBuilder.Entity<GradeBookDto>()
                .HasOne(cc => cc.User)
                .WithMany(c => c.GradeBooks)
                .HasForeignKey(c => c.UserId);

            modelBuilder.Entity<GradeBookDto>()
                .HasOne(cc => cc.Course)
                .WithMany(c => c.GradeBooks)
                .HasForeignKey(cc => cc.CourseId);
        }
    }
}
