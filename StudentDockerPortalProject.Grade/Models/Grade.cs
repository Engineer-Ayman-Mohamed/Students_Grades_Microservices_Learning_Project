namespace StudentDockerPortalProject.Grade.Models;

public class Grade
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public required string CourseName { get; set; }
    public decimal Score { get; set; }
    public DateTime GradeDate { get; set; }
    public string? Notes { get; set; }
}