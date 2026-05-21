// ═══════════════════════════════════════════════════════════════
// Grade Entity — Core domain model for grade records
// ═══════════════════════════════════════════════════════════════
// Mapped to the [Grades].[Grades] table. StudentId is a logical
// reference to the Students service (no FK constraint — bounded
// context separation).
// ═══════════════════════════════════════════════════════════════

namespace StudentDockerPortalProject.Grade.Models;

public class Grade
{
    public int Id { get; set; }
    public int StudentId { get; set; }            // References student in the other microservice
    public required string CourseName { get; set; }
    public double Score { get; set; }
    public DateTime GradeDate { get; set; }
    public string? Notes { get; set; }
}