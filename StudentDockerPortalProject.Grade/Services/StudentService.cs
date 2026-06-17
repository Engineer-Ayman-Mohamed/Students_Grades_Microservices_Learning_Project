// ═══════════════════════════════════════════════════════════════
// StudentService — HTTP Client for Students Microservice
// ═══════════════════════════════════════════════════════════════
// Typed HttpClient implementation of IStudentService. Calls the
// Students service endpoints (students/getall, students/getbyid/{id})
// and deserializes JSON responses into StudentDto records. The
// base URL and JSON acceptance header are configured in Program.cs
// via AddHttpClient.
// ═══════════════════════════════════════════════════════════════

using Microsoft.AspNetCore.Mvc;
using StudentDockerPortalProject.Grade.Models.Dtos;

namespace StudentDockerPortalProject.Grade.Services;

public class StudentService : IStudentService
{
    private readonly HttpClient _httpClient;
    public StudentService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    public async Task<IReadOnlyList<StudentDto>> GetAllStudentsAsync()
    {
        var students = await _httpClient.GetFromJsonAsync<IReadOnlyList<StudentDto>>("students/getall");
        return students ?? []; 
    }
    public Task<StudentDto>? GetStudentByIdAsync(int id)
    {
        return _httpClient.GetFromJsonAsync<StudentDto>($"students/getbyid/{id}")!;
    }
}