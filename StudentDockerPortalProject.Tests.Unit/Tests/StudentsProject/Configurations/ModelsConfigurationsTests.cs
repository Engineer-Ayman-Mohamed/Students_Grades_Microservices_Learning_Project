using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using StudentDockerPortalProject.Students.Models;
using StudentDockerPortalProject.Students.Models.Configurations;

namespace StudentDockerPortalProject.Tests.Unit.Tests.StudentsProject.Configurations;

public class ModelsConfigurationsTests
{
    private static IModel BuildStudentModel()
    {
        var modelBuilder = new ModelBuilder(); 
        var config = new StudentConfiguration(); // student configuration 
        config.Configure(modelBuilder.Entity<Student>());
        return (IModel)modelBuilder.Model;
    }
    [Fact]
    public void StudentConfiguration_sets_primary_key_on_id()
    {
        var model = BuildStudentModel();

        var entityType = model.FindEntityType(typeof(Student))!;
        var primaryKey = entityType.FindPrimaryKey()!;

        primaryKey.Properties.Should().HaveCount(1);
        primaryKey.Properties[0].Name.Should().Be("Id");
    }

    [Fact]
    public void StudentConfiguration_sets_firstname_required_and_max_length_50()
    {
        var model = BuildStudentModel();
        var studentEntity = model.FindEntityType(typeof(Student))!;
        var firstNameProperty = studentEntity.FindProperty(name: nameof(Student.FirstName))!;
        firstNameProperty.GetMaxLength().Should().Be(50);
    }
    [Fact]
    public void StudentConfiguration_sets_lastname_required_and_max_length_50()
    {
        var model = BuildStudentModel();
        var entityType = model.FindEntityType(typeof(Student))!;
        var lastName = entityType.FindProperty(nameof(Student.LastName))!;

        lastName.GetMaxLength().Should().Be(50);
    }
    [Fact]
    public void StudentConfiguration_sets_email_required_and_max_length_100()
    {
        var model = BuildStudentModel();
        var entityType = model.FindEntityType(typeof(Student))!;
        var email = entityType.FindProperty(nameof(Student.Email))!;

        email.GetMaxLength().Should().Be(100);
    }
    [Fact]
    public void StudentConfiguration_creates_unique_email_index()
    {
        var model = BuildStudentModel();

        var entityType = model.FindEntityType(typeof(Student))!;
        var indexes = entityType.GetIndexes().ToList();

        var emailIndex = indexes.FirstOrDefault(idx =>
            idx.Properties.Any(p => p.Name == nameof(Student.Email)));

        emailIndex.Should().NotBeNull("a unique index should exist on Email");
        emailIndex!.IsUnique.Should().BeTrue();
    }

}