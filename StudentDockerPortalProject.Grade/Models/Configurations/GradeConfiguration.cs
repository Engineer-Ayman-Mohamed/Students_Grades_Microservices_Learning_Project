// ═══════════════════════════════════════════════════════════════
// Grade Entity Configuration — EF Core Fluent API
// ═══════════════════════════════════════════════════════════════
// Defines column constraints, types, and an index on StudentId
// (for efficient lookups by student). Applied automatically via
// ApplyConfigurationsFromAssembly in GradeDatabaseContext.
// ═══════════════════════════════════════════════════════════════

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
            .HasColumnType("decimal(10,2)");          // Precision for grade scores
        builder.Property(g => g.GradeDate)
            .HasColumnType("date");                    // Date only, no time component
        builder.Property(g => g.Notes)
            .HasMaxLength(500);
        builder.HasIndex(g => g.StudentId);       // Index for querying grades by student
    }
}