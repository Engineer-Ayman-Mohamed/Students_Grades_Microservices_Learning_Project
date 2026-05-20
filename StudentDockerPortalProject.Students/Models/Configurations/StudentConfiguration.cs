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

        builder.HasIndex(s => s.Email).IsUnique();

        builder.Property(s => s.DateOfBirth)
               .HasColumnType("date");

        builder.Property(s => s.EnrollmentDate)
               .HasColumnType("date")
               .HasDefaultValueSql("getdate()");;
    }
}
