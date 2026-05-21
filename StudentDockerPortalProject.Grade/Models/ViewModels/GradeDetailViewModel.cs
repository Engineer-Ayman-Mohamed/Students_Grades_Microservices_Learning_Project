namespace StudentDockerPortalProject.Grade.Models.ViewModels;

public class GradeDetailViewModel
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public string? StudentName { get; set; }
    public required string CourseName { get; set; }
    public double Score { get; set; }
    public DateTime GradeDate { get; set; }
    public string? Notes { get; set; }
    public string? ErrorMessage { get; set; }
}
