// ═══════════════════════════════════════════════════════════════
// IStudentService — Interface for Students API Client
// ═══════════════════════════════════════════════════════════════
// Abstraction over HTTP calls to the Students microservice.
// Implemented by StudentService using a typed HttpClient.
// ═══════════════════════════════════════════════════════════════

using Microsoft.AspNetCore.Mvc;
using StudentDockerPortalProject.Grade.Models.Dtos;

namespace StudentDockerPortalProject.Grade.Services;

public interface IStudentService
{
    Task<IReadOnlyList<StudentDto>> GetAllStudentsAsync();
    Task<StudentDto>? GetStudentByIdAsync(int id);
}
