using Microsoft.AspNetCore.Mvc.Rendering;

namespace StudentDockerPortalProject.Grade.Models.ViewModels;

public class GradeFormViewModel
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public string CourseName { get; set; } = string.Empty;
    public decimal Score { get; set; }
    public DateTime GradeDate { get; set; } = DateTime.Today;
    public string? Notes { get; set; }
    public List<SelectListItem> StudentOptions { get; set; } = [];
    public string? ErrorMessage { get; set; }
}
