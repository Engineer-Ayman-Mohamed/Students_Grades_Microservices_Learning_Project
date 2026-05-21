using Microsoft.EntityFrameworkCore;

namespace StudentDockerPortalProject.Grade.Data;

public class GradeDatabaseContext : DbContext
{
    public GradeDatabaseContext(DbContextOptions<GradeDatabaseContext> options) 
        : base(options) { }
    public DbSet<Models.Grade> Grades => Set<Models.Grade>();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("Grades");
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(GradeDatabaseContext).Assembly);
    }
}