// ═══════════════════════════════════════════════════════════════
// Grades Microservice — Application Entry Point
// ═══════════════════════════════════════════════════════════════
// Configures ASP.NET Core MVC, EF Core with SQL Server (Grade
// database), and a typed HttpClient (StudentService) to communicate
// with the Students microservice over HTTP. Auto-migrates on startup.
// ═══════════════════════════════════════════════════════════════

using System.Net.Http.Headers;
using Microsoft.EntityFrameworkCore;
using StudentDockerPortalProject.Grade.Data;
using StudentDockerPortalProject.Grade.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<GradeDatabaseContext>(options =>
{
    options
        .UseSqlServer(builder.Configuration.GetConnectionString("StudentDbConnectionString"))
        .EnableSensitiveDataLogging()
        .LogTo(Console.WriteLine, LogLevel.Information);
});

// Register typed HttpClient for inter-service communication
builder.Services.AddHttpClient<IStudentService, StudentService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("StudentsApi:BaseUrl") ?? "http://localhost:5167");
    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
});
var app = builder.Build();

// Auto-migrate the Grade database schema
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<GradeDatabaseContext>();
    context.Database.Migrate();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();