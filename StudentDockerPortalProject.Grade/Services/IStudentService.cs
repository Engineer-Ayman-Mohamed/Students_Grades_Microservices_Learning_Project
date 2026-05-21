using Microsoft.AspNetCore.Mvc;
using StudentDockerPortalProject.Grade.Models.Dtos;

namespace StudentDockerPortalProject.Grade.Services;

public interface IStudentService
{
    Task<IReadOnlyList<StudentDto>> GetAllStudentsAsync();
    Task<StudentDto>? GetStudentByIdAsync(int id);
}   