// ═══════════════════════════════════════════════════════════════
// GradeIndexViewModel — ViewModel for the grades list page
// ═══════════════════════════════════════════════════════════════
// GradeIndexViewModel holds the list of displayable grades and an
// optional error message when the Students API is unreachable.
// GradeRowViewModel is the per-row display projection with a
// resolved student name.
// ═══════════════════════════════════════════════════════════════

namespace StudentDockerPortalProject.Grade.Models.ViewModels;

public class GradeIndexViewModel
{
    public List<GradeRowViewModel> Grades { get; set; } = [];
    public string? ErrorMessage { get; set; }          // Set when student name resolution fails
}

public class GradeRowViewModel
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public string? StudentName { get; set; }           // Resolved from Students API, null if unavailable
    public required string CourseName { get; set; }
    public double Score { get; set; }
    public DateTime GradeDate { get; set; }
    public string? Notes { get; set; }
}
