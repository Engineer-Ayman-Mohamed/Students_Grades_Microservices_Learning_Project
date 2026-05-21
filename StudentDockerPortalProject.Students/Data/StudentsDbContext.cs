using Microsoft.EntityFrameworkCore;
using StudentDockerPortalProject.Students.Models;
using StudentDockerPortalProject.Students.Models.Configurations;

namespace StudentDockerPortalProject.Students.Data;

public class StudentsDbContext : DbContext
{
    public StudentsDbContext(DbContextOptions<StudentsDbContext> options) : base(options) { }

    public DbSet<Student> Students => Set<Student>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("Students");
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(StudentsDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
