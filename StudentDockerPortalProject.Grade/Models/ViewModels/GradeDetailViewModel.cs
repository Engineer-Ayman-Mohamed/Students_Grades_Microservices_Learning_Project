// ═══════════════════════════════════════════════════════════════
// GradeDetailViewModel — ViewModel for grade detail/delete pages
// ═══════════════════════════════════════════════════════════════
// Displays a single grade with the resolved student name (fetched
// from the Students API). ErrorMessage is shown if the API call
// fails.
// ═══════════════════════════════════════════════════════════════

namespace StudentDockerPortalProject.Grade.Models.ViewModels;

public class GradeDetailViewModel
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public string? StudentName { get; set; }        // Resolved from Students API
    public required string CourseName { get; set; }
    public double Score { get; set; }
    public DateTime GradeDate { get; set; }
    public string? Notes { get; set; }
    public string? ErrorMessage { get; set; }
}
