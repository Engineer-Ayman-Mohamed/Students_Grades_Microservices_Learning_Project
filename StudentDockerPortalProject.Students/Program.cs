// ═══════════════════════════════════════════════════════════════
// Students Microservice — Application Entry Point
// ═══════════════════════════════════════════════════════════════
// Configures ASP.NET Core MVC, EF Core with SQL Server (with retry
// logic for container startup), Swagger for REST API documentation,
// and auto-applies pending migrations on startup.
// ═══════════════════════════════════════════════════════════════

using Microsoft.EntityFrameworkCore;
using StudentDockerPortalProject.Students.Data;
using System.Reflection;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<StudentsDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("StudentDbConnectionString"), sqlOptions =>
        {
          sqlOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), null);
          sqlOptions.MigrationsHistoryTable("__EFMigrationsHistory", "dbo");
          // Retry up to 10 times with 30s delays — handles SQL Server container startup race
          sqlOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), null);  
        })
        .EnableSensitiveDataLogging()
        .LogTo(Console.WriteLine, LogLevel.Information);
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Student Portal API",
        Version = "v1",
        Description = "REST API for managing students in the Student Portal."
    });
});
var app = builder.Build();

// Auto-migrate database on startup — schema stays in sync without manual scripts
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<StudentsDbContext>();
    context.Database.Migrate();
}

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Student Portal API v1");
});

if (app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseExceptionHandler("/Home/Error");
app.UseStaticFiles();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
