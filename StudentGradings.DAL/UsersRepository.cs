﻿using Microsoft.EntityFrameworkCore;
using StudentGradings.CORE;
using StudentGradings.DAL.Interfaces;
using StudentGradings.DAL.Models.Dtos;

namespace StudentGradings.DAL;

public class UsersRepository(StudentGradingsContext context) : IUsersRepository
{
    public async Task<Guid> AddUserAsync(UserDto user)
    {
        context.Users.Add(user);
        await context.SaveChangesAsync();
        return user.Id;
    }

    public async Task UpdateUserAsync(UserDto user, UserDto changeUser)
    {
        var existingUser = await context.Users.FirstOrDefaultAsync(c => c.Id == user.Id);
        existingUser.Name = changeUser.Name;
        existingUser.LastName = changeUser.LastName;
        existingUser.Phone = changeUser.Phone;
        existingUser.Email = changeUser.Email;
        await context.SaveChangesAsync();
    }

    public async Task UpdatePasswordByUserIdAsync(UserDto user, string password)
    {
        user.Password = password;
        await context.SaveChangesAsync();
    }

    public async Task<UserDto?> GetUserByIdAsync(Guid id) => await context.Users.SingleOrDefaultAsync(c => c.Id == id);

    public async Task<UserDto?> GetUserByEmailAsync(string email) => await context.Users.SingleOrDefaultAsync(c => c.Email == email);

    public async Task SetUserRoleByUserIdAsync(UserDto user, UserRole role)
    {
        user.Role = role;
        await context.SaveChangesAsync();
    }

    public async Task<List<UserDto>> GetAllUsersAsync()
    {
        return await context.Users.Where(c => c.IsDeactivated == false).ToListAsync() ?? new List<UserDto>();
    }

    public async Task<UserDto?> GetUserWithCoursesAndGradesAsync(Guid id) =>
        await context.Users
        .Include(c => c.GradeBooks)
        .ThenInclude(c => c.Course)
        .SingleOrDefaultAsync(c => c.Id == id);

    public async Task DeactivateUserAsync(UserDto user)
    {
        var existingUser = await context.Users.FirstOrDefaultAsync(c => c.Id == user.Id);
        existingUser.IsDeactivated = true;
        await context.SaveChangesAsync();
    }

    public async Task DeleteUserAsync(UserDto user)
    {
        var userToRemove = await context.Users.FirstOrDefaultAsync(c => c.Id == user.Id);
        context.Users.Remove(userToRemove);
        await context.SaveChangesAsync();
    }
}