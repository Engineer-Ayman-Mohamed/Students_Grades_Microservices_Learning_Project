// ═══════════════════════════════════════════════════════════════
// Student Entity Configuration — EF Core Fluent API
// ═══════════════════════════════════════════════════════════════
// Defines column constraints, types, and the unique email index
// for the Student entity. Applied automatically via
// ApplyConfigurationsFromAssembly in StudentsDbContext.
// ═══════════════════════════════════════════════════════════════

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace StudentDockerPortalProject.Students.Models.Configurations;

public class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.FirstName)
               .IsRequired()
               .HasMaxLength(50);

        builder.Property(s => s.LastName)
               .IsRequired()
               .HasMaxLength(50);

        builder.Property(s => s.Email)
               .IsRequired()
               .HasMaxLength(100);

        builder.HasIndex(s => s.Email).IsUnique();  // Enforce unique email constraint at DB level

        builder.Property(s => s.DateOfBirth)
               .HasColumnType("date");

        builder.Property(s => s.EnrollmentDate)
               .HasColumnType("date")
               .HasDefaultValueSql("getdate()");;  // Automatically set on insert
    }
}
