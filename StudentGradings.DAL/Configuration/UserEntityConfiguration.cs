using Microsoft.EntityFrameworkCore;
using StudentGradings.DAL.Models.Dtos;

namespace StudentGradings.DAL.Configuration;

internal static class UserEntityConfiguration
{
    internal static void AddUserEntityConfiguration(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UserDto>()
            .ToTable("Users")
            .HasKey(c => c.Id);

        modelBuilder.Entity<UserDto>()
            .Property(c => c.Name)
            .HasMaxLength(49);

        modelBuilder.Entity<UserDto>()
            .Property(c => c.LastName)
            .HasMaxLength(49);

        modelBuilder.Entity<UserDto>()
            .Property(c => c.Phone)
            .HasMaxLength(18);

        modelBuilder.Entity<UserDto>()
            .Property(c => c.Password)
            .IsRequired()
            .HasMaxLength(18);

        modelBuilder.Entity<UserDto>()
            .HasIndex(c => c.Email).IsUnique();

        modelBuilder.Entity<UserDto>()
            .Property(c => c.Email)
            .IsRequired()
            .HasMaxLength(49);
    }
}
