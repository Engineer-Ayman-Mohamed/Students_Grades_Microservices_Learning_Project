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
        var students = await _httpClient.GetFromJsonAsync<IReadOnlyList<StudentDto>>("api/students");
        return students ?? []; 
    }
    public async Task<StudentDto?> GetStudentByIdAsync(int id)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<StudentDto>($"api/students/{id}");
        }
        catch
        {
            return null;
        }
    }
}