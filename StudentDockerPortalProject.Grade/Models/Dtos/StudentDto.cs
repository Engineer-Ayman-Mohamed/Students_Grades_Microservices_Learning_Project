// ═══════════════════════════════════════════════════════════════
// StudentDto — DTO for deserializing Students API responses
// ═══════════════════════════════════════════════════════════════
// Mirrors the StudentDto from the Students service. Used by
// StudentService (HttpClient) to deserialize JSON responses
// from the Students microservice REST API.
// ═══════════════════════════════════════════════════════════════

namespace StudentDockerPortalProject.Grade.Models.Dtos;

public record StudentDto(
    int Id,
    string FirstName,
    string LastName,
    string Email,
    DateTime DateOfBirth,
    DateTime EnrollmentDate
);
