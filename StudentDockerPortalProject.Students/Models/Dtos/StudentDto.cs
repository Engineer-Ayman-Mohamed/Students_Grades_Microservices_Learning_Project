namespace StudentDockerPortalProject.Students.Models.Dtos;

public record StudentDto(
    int Id,
    string FirstName,
    string LastName,
    string Email,
    DateTime DateOfBirth,
    DateTime EnrollmentDate
);

public record CreateStudentRequest(
    string FirstName,
    string LastName,
    string Email,
    DateTime DateOfBirth,
    DateTime EnrollmentDate
);

public record UpdateStudentRequest(
    string FirstName,
    string LastName,
    string Email,
    DateTime DateOfBirth,
    DateTime EnrollmentDate
);
