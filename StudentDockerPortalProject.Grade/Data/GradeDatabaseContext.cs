// ═══════════════════════════════════════════════════════════════
// Grade DbContext — EF Core Database Context
// ═══════════════════════════════════════════════════════════════
// Manages the Grade entity set under the "Grades" schema. Applies
// GradeConfiguration and any other IEntityTypeConfiguration found
// in this assembly.
// ═══════════════════════════════════════════════════════════════

using Microsoft.EntityFrameworkCore;

namespace StudentDockerPortalProject.Grade.Data;

public class GradeDatabaseContext : DbContext
{
    public GradeDatabaseContext(DbContextOptions<GradeDatabaseContext> options) 
        : base(options) { }
    public DbSet<Models.Grade> Grades => Set<Models.Grade>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("Grades");          // All tables under [Grades] schema
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(GradeDatabaseContext).Assembly);
    }
}