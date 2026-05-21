using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace StudentDockerPortalProject.Grade.Models.Configurations;

public class GradeConfiguration : IEntityTypeConfiguration<Grade>
{
    public void Configure(EntityTypeBuilder<Grade> builder)
    {
        builder.HasKey(g => g.Id);
        builder.Property(g => g.CourseName)
            .IsRequired()
            .HasMaxLength(100);
        builder.Property(g => g.Score)
            .HasColumnType("decimal(10,2)");
        builder.Property(g => g.GradeDate)
            .HasColumnType("date");
        builder.Property(g => g.Notes)
            .HasMaxLength(500);
        builder.HasIndex(g => g.StudentId);
    }
}