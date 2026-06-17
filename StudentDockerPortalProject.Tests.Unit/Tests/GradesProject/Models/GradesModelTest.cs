using FluentAssertions;
namespace StudentDockerPortalProject.Tests.Unit.Tests.GradesProject.Models;
public class GradesModelTest
{
    [Fact]
    public void Grade_can_be_created_with_valid_data()
    {
        var grade = new Grade.Models.Grade
        {
            Id = 1,
            StudentId = 10,
            CourseName = "Mathematics",
            Score = 92.5,
            GradeDate = new DateTime(2024, 6, 15),
            Notes = "Excellent performance"
        };

        grade.Id.Should().Be(1);
        grade.StudentId.Should().Be(10);
        grade.CourseName.Should().Be("Mathematics");
        grade.Score.Should().Be(92.5);
        grade.GradeDate.Should().Be(new DateTime(2024, 6, 15));
        grade.Notes.Should().Be("Excellent performance");
    }
    [Fact]
    public void Grade_notes_can_be_null()
    {
        var grade = new Grade.Models.Grade
        {
            StudentId = 10,
            CourseName = "Physics",
            Score = 85.0,
            GradeDate = DateTime.Today,
            Notes = null
        };

        grade.Notes.Should().BeNull();
    }
    [Fact]
    public void Grade_notes_can_be_empty_string()
    {
        var grade = new Grade.Models.Grade
        {
            StudentId = 10,
            CourseName = "Chemistry",
            Score = 78.0,
            GradeDate = DateTime.Today,
            Notes = ""
        };

        grade.Notes.Should().BeEmpty();
    }
    [Fact]
    public void Grade_score_stores_decimal_values()
    {
        var grade = new Grade.Models.Grade
        {
            StudentId = 10,
            CourseName = "English",
            Score = 95.7,
            GradeDate = DateTime.Today
        };

        grade.Score.Should().Be(95.7);
    }
    [Fact]
    public void Grade_score_can_be_zero()
    {
        var grade = new Grade.Models.Grade
        {
            StudentId = 10,
            CourseName = "History",
            Score = 0,
            GradeDate = DateTime.Today
        };

        grade.Score.Should().Be(0);
    }
    [Fact]
    public void Grade_score_can_be_negative()
    {
        var grade = new Grade.Models.Grade
        {
            StudentId = 10,
            CourseName = "Detention",
            Score = -5.0,
            GradeDate = DateTime.Today
        };
        grade.Score.Should().Be(-5.0);
    }
    [Fact]
    public void Grade_student_id_stores_correctly()
    {
        var grade = new Grade.Models.Grade
        {
            StudentId = 42,
            CourseName = "Biology",
            Score = 88.0,
            GradeDate = DateTime.Today
        };
        grade.StudentId.Should().Be(42);
    }
    [Fact]
    public void Grade_date_stores_exact_datetime()
    {
        var specificDate = new DateTime(2024, 12, 31);
        var grade = new Grade.Models.Grade
        {
            StudentId = 10,
            CourseName = "Year End",
            Score = 100.0,
            GradeDate = specificDate
        };
        grade.GradeDate.Should().Be(specificDate);
    }
}