﻿using StudentGradings.DAL.Models.Dtos;

namespace StudentGradings.DAL.Interfaces
{
    public interface IGradeBooksRepository
    {
        Task<Guid> AddGradeBookAsync(GradeBookDto gradeBook);
        Task AddGradeByCourseIdAndUserIdAsync(GradeBookDto gradeBook);
        Task UpdateGradeByCourseIdAndUserIdAsync(GradeBookDto gradeBook, float grade);
        Task<GradeBookDto?> GetGradeBookAsync(Guid courseId, Guid userId);
        Task<List<GradeBookDto>> GetGradesByCourseIdAsync(Guid courseId);
        Task<List<GradeBookDto>> GetAllGradesWithCoursesAsync();
        Task<bool> GradeExistsByCourseIdAndUserIdAsync(Guid courseId, Guid userId);
        Task DeleteGradeByCourseIdAndUserIdAsync(Guid courseId, Guid userId);
    }
}