using FluentAssertions;
using StudentDockerPortalProject.Grade.Models.ViewModels;

namespace StudentDockerPortalProject.Tests.Unit.Tests.GradesProject.ViewModels;

public class ViewModelsTests
{
    [Fact]
    public void GradeIndexViewModel_defaults_to_empty_grades_list()
    {
        var vm = new GradeIndexViewModel();

        vm.Grades.Should().NotBeNull();
        vm.Grades.Should().BeEmpty();
    }
    [Fact]
    public void GradeIndexViewModel_error_message_defaults_to_null()
    {
        var vm = new GradeIndexViewModel();

        vm.ErrorMessage.Should().BeNull();
    }
    [Fact]
    public void GradeFormViewModel_grade_date_defaults_to_today()
    {
        var vm = new GradeFormViewModel();

        vm.GradeDate.Should().Be(DateTime.Today);
    }
    [Fact]
    public void GradeFormViewModel_student_options_defaults_to_empty()
    {
        var vm = new GradeFormViewModel();

        vm.StudentOptions.Should().NotBeNull();
        vm.StudentOptions.Should().BeEmpty();
    }
    [Fact]
    public void GradeFormViewModel_course_name_defaults_to_empty_string()
    {
        var vm = new GradeFormViewModel();

        vm.CourseName.Should().Be(string.Empty);
    }
    [Fact]
    public void GradeDetailViewModel_stores_properties_correctly()
    {
        var vm = new GradeDetailViewModel
        {
            Id = 5,
            StudentId = 10,
            StudentName = "John Doe",
            CourseName = "Math",
            Score = 95.5,
            GradeDate = new DateTime(2024, 6, 15),
            Notes = "Great work"
        };

        vm.Id.Should().Be(5);
        vm.StudentId.Should().Be(10);
        vm.StudentName.Should().Be("John Doe");
        vm.CourseName.Should().Be("Math");
        vm.Score.Should().Be(95.5);
        vm.GradeDate.Should().Be(new DateTime(2024, 6, 15));
        vm.Notes.Should().Be("Great work");
    }
    [Fact]
    public void GradeDetailViewModel_error_message_defaults_to_null()
    {
        var vm = new GradeDetailViewModel
        {
            CourseName = "Test"
        };
        
        vm.ErrorMessage.Should().BeNull();
    }
}