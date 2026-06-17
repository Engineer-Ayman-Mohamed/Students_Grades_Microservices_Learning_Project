// ═══════════════════════════════════════════════════════════════
// Student DTOs — API Request/Response Records
// ═══════════════════════════════════════════════════════════════
// StudentDto is the API response shape. Create/Update records are
// used as [FromBody] parameters for POST/PUT endpoints. All use
// C# 12 primary-constructor positional records for brevity.
// ═══════════════════════════════════════════════════════════════

namespace StudentDockerPortalProject.Students.Models.Dtos;

// API response — read-only projection of the Student entity
public record StudentDto(
    int Id,
    string FirstName,
    string LastName,
    string Email,
    DateTime DateOfBirth,
    DateTime EnrollmentDate
);

// POST /api/students request body
public record CreateStudentRequest(
    string FirstName,
    string LastName,
    string Email,
    DateTime DateOfBirth,
    DateTime EnrollmentDate
);

// PUT /api/students/{id} request body
public record UpdateStudentRequest(
    string FirstName,
    string LastName,
    string Email,
    DateTime DateOfBirth,
    DateTime EnrollmentDate
);
