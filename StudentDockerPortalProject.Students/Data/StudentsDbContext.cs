// ═══════════════════════════════════════════════════════════════
// Students DbContext — EF Core Database Context
// ═══════════════════════════════════════════════════════════════
// Manages the Students entity set and applies all configurations
// from the assembly. Uses the "Students" schema to keep student
// data isolated in its own schema namespace.
// ═══════════════════════════════════════════════════════════════

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
        modelBuilder.HasDefaultSchema("Students");       // All tables under [Students] schema
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(StudentsDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
