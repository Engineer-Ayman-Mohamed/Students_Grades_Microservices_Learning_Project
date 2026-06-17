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