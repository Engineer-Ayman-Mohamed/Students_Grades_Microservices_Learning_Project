using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using StudentDockerPortalProject.Grade.Models.Configurations;

namespace StudentDockerPortalProject.Tests.Unit.Tests.GradesProject.Configurations;

public class ModelsConfigurationsTests
{
    private static IModel BuildGradeModel()
    {
        var modelBuilder = new ModelBuilder();
        var config = new GradeConfiguration();
        config.Configure(modelBuilder.Entity<Grade.Models.Grade>());
        return (IModel)modelBuilder.Model;
    }

    [Fact]
    public void GradeConfiguration_sets_primary_key_on_id()
    {
        var model = BuildGradeModel();
        var gradeEntity = model.FindEntityType(typeof(Grade.Models.Grade));
        var primaryKey = gradeEntity!.FindPrimaryKey();
        
        primaryKey!.IsPrimaryKey().Should().BeTrue();
        primaryKey!.Properties.Should().HaveCount(1);
        primaryKey.Properties[0].Name.Should().Be("Id");
    }
    [Fact]
    public void GradeConfiguration_sets_coursename_required_and_max_length_100()
    {
        var model = BuildGradeModel();
        var entityType = model.FindEntityType(typeof(Grade.Models.Grade))!;
        var courseName = entityType.FindProperty(nameof(Grade.Models.Grade.CourseName))!;

        courseName.GetMaxLength().Should().Be(100);
    }
    [Fact]
    public void GradeConfiguration_sets_notes_max_length_500_and_not_required()
    {
        var model = BuildGradeModel();
        var entityType = model.FindEntityType(typeof(Grade.Models.Grade))!;
        var notes = entityType.FindProperty(nameof(Grade.Models.Grade.Notes))!;

        notes.GetMaxLength().Should().Be(500);
    }
    [Fact]
    public void GradeConfiguration_creates_index_on_studentid()
    {
        var model = BuildGradeModel();
        var entityType = model.FindEntityType(typeof(Grade.Models.Grade))!;
        var indexes = entityType.GetIndexes().ToList();

        var studentIdIndex = indexes.FirstOrDefault(idx =>
            idx.Properties.Any(p => p.Name == nameof(Grade.Models.Grade.StudentId)));

        studentIdIndex.Should().NotBeNull("an index should exist on StudentId");
    }
}