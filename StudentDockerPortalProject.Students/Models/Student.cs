// ═══════════════════════════════════════════════════════════════
// Student Entity — Core domain model
// ═══════════════════════════════════════════════════════════════
// Mapped to the [Students].[Students] table via EF Core. All string
// properties are required (enforced by C# 11 required modifier).
// ═══════════════════════════════════════════════════════════════

namespace StudentDockerPortalProject.Students.Models;

public class Student
{
    public int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public DateTime DateOfBirth { get; set; }
    public DateTime EnrollmentDate { get; set; }
}
