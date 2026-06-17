using FluentAssertions;
using StudentDockerPortalProject.Students.Models.Dtos;

namespace StudentDockerPortalProject.Tests.Unit.Tests.StudentsProject.DTO;

public class StudentDtoTest
{
    [Fact]
    public void StudentDto_records_with_same_values_are_equal()
    {
        var date = new DateTime(2000, 1, 1);
        var dto1 = new StudentDto(1, "John", "Doe", "john@test.com", date, date);
        var dto2 = new StudentDto(1, "John", "Doe", "john@test.com", date, date);

        dto1.Should().Be(dto2);
        (dto1 == dto2).Should().BeTrue();
    }
    [Fact]
    public void StudentDto_records_with_different_values_are_not_equal()
    {
        var date = new DateTime(2000, 1, 1);
        var dto1 = new StudentDto(1, "John", "Doe", "john@test.com", date, date);
        var dto2 = new StudentDto(2, "Jane", "Smith", "jane@test.com", date, date);

        dto1.Should().NotBe(dto2);
        (dto1 != dto2).Should().BeTrue();
    }
    [Fact]
    public void StudentDto_tostring_contains_property_values()
    {
        var dto = new StudentDto(1, "John", "Doe", "john@test.com",
            new DateTime(2000, 1, 1), new DateTime(2024, 1, 1));
        var str = dto.ToString();

        str.Should().Contain("John");
        str.Should().Contain("Doe");
        str.Should().Contain("john@test.com");
    }
    [Fact]
    public void CreateStudentRequest_stores_all_properties()
    {
        var dob = new DateTime(1998, 3, 14);
        var enroll = new DateTime(2024, 8, 1);
        var request = new CreateStudentRequest("Alice", "Wonder", "alice@test.com", dob, enroll);

        request.FirstName.Should().Be("Alice");
        request.LastName.Should().Be("Wonder");
        request.Email.Should().Be("alice@test.com");
        request.DateOfBirth.Should().Be(dob);
        request.EnrollmentDate.Should().Be(enroll);
    }
    [Fact]
    public void UpdateStudentRequest_stores_all_properties()
    {
        var dob = new DateTime(1999, 7, 20);
        var enroll = new DateTime(2024, 9, 1);

        var request = new UpdateStudentRequest("Bob", "Builder", "bob@test.com", dob, enroll);

        request.FirstName.Should().Be("Bob");
        request.LastName.Should().Be("Builder");
        request.Email.Should().Be("bob@test.com");
        request.DateOfBirth.Should().Be(dob);
        request.EnrollmentDate.Should().Be(enroll);
    }
    [Fact]
    public void CreateStudentRequest_and_UpdateStudentRequest_are_different_types()
    {
        var date = new DateTime(2000, 1, 1);

        var create = new CreateStudentRequest("Test", "User", "test@test.com", date, date);
        var update = new UpdateStudentRequest("Test", "User", "test@test.com", date, date);

        create.Equals(update).Should().BeFalse();
    }
}