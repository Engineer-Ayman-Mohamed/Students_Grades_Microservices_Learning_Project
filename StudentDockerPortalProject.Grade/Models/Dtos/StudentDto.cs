namespace StudentDockerPortalProject.Grade.Models.Dtos;

public record StudentDto(
    int Id,
    string FirstName,
    string LastName,
    string Email,
    DateTime DateOfBirth,
    DateTime EnrollmentDate
);
