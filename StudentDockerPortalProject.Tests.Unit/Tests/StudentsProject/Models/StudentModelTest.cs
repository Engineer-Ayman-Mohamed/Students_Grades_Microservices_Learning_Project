using FluentAssertions;
using StudentDockerPortalProject.Students.Models;

namespace StudentDockerPortalProject.Tests.Unit.Tests.StudentsProject.Models;
public class StudentModelTest
{
    [Fact]
    public void Student_can_be_created_with_valid_data()
    {
        var student = new Student
        {
            Id = 1,
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@example.com",
            DateOfBirth = new DateTime(2000, 5, 15),
            EnrollmentDate = new DateTime(2024, 1, 10)
        };
        student.Id.Should().Be(1);
        student.FirstName.Should().Be("John");
        student.LastName.Should().Be("Doe");
        student.Email.Should().Be("john.doe@example.com");
        student.DateOfBirth.Should().Be(new DateTime(2000, 5, 15));
        student.EnrollmentDate.Should().Be(new DateTime(2024, 1, 10));
    }
    [Fact]
    public void Student_first_name_can_be_set_and_retrieved()
    {
        var student = new Student
        {
            FirstName = "Alice",
            LastName = "Smith",
            Email = "alice@test.com",
            DateOfBirth = DateTime.MinValue,
            EnrollmentDate = DateTime.MinValue
        };

        student.FirstName.Should().Be("Alice");
    }
    [Fact]
    public void Student_last_name_can_be_set_and_retrieved()
    {
        var student = new Student
        {
            FirstName = "Bob",
            LastName = "Johnson",
            Email = "bob@test.com",
            DateOfBirth = DateTime.MinValue,
            EnrollmentDate = DateTime.MinValue
        };

        student.LastName.Should().Be("Johnson");
    }
    [Fact]
    public void Student_email_can_be_set_and_retrieved()
    {
        var student = new Student
        {
            FirstName = "Test",
            LastName = "User",
            Email = "test.user@company.org",
            DateOfBirth = DateTime.MinValue,
            EnrollmentDate = DateTime.MinValue
        };

        student.Email.Should().Be("test.user@company.org");
    }
    [Fact]
    public void Student_date_of_birth_stores_exact_datetime()
    {
        var specificDate = new DateTime(1995, 12, 25, 14, 30, 0);

        var student = new Student
        {
            FirstName = "Christmas",
            LastName = "Baby",
            Email = "xmas@test.com",
            DateOfBirth = specificDate,
            EnrollmentDate = DateTime.MinValue
        };

        student.DateOfBirth.Should().Be(specificDate);
    }
    [Fact]
    public void Student_enrollment_date_stores_exact_datetime()
    {
        var specificDate = new DateTime(2024, 9, 1);

        var student = new Student
        {
            FirstName = "Fall",
            LastName = "Student",
            Email = "fall@test.com",
            DateOfBirth = DateTime.MinValue,
            EnrollmentDate = specificDate
        };

        student.EnrollmentDate.Should().Be(specificDate);
    }
    [Fact]
    public void Student_id_defaults_to_zero_before_persisted()
    {
        var student = new Student
        {
            FirstName = "New",
            LastName = "Student",
            Email = "new@test.com",
            DateOfBirth = DateTime.MinValue,
            EnrollmentDate = DateTime.MinValue
        };

        student.Id.Should().Be(0);
    }
    [Fact]
    public void Student_properties_are_independent()
    {
        var student = new Student
        {
            FirstName = "Original",
            LastName = "Name",
            Email = "original@test.com",
            DateOfBirth = new DateTime(2000, 1, 1),
            EnrollmentDate = new DateTime(2024, 1, 1)
        };

        student.FirstName = "Changed";
        
        student.Id.Should().Be(0);
        student.LastName.Should().Be("Name");
        student.Email.Should().Be("original@test.com");
        student.DateOfBirth.Should().Be(new DateTime(2000, 1, 1));
        student.EnrollmentDate.Should().Be(new DateTime(2024, 1, 1));
    }
}