// ═══════════════════════════════════════════════════════════════
// GradeFormViewModel — ViewModel for Create/Edit grade forms
// ═══════════════════════════════════════════════════════════════
// Carries form fields, the student dropdown options (populated
// from the Students API), and an optional error message if the
// student list could not be fetched.
// ═══════════════════════════════════════════════════════════════

using Microsoft.AspNetCore.Mvc.Rendering;

namespace StudentDockerPortalProject.Grade.Models.ViewModels;

public class GradeFormViewModel
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public string CourseName { get; set; } = string.Empty;
    public double Score { get; set; }
    public DateTime GradeDate { get; set; } = DateTime.Today;
    public string? Notes { get; set; }
    public List<SelectListItem> StudentOptions { get; set; } = [];   // Dropdown populated via HTTP
    public string? ErrorMessage { get; set; }
}
