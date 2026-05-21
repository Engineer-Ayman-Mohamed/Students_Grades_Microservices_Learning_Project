// ═══════════════════════════════════════════════════════════════
// Grades Controller — MVC CRUD with Inter-Service Integration
// ═══════════════════════════════════════════════════════════════
// Full CRUD for the Grade entity. Uses IStudentService (HTTP client)
// to fetch student names from the Students microservice for display
// in the grades list, detail views, and form dropdowns. All student
// API calls are wrapped in try/catch with graceful degradation.
// ═══════════════════════════════════════════════════════════════

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentDockerPortalProject.Grade.Data;
using StudentDockerPortalProject.Grade.Models;
using StudentDockerPortalProject.Grade.Models.ViewModels;
using StudentDockerPortalProject.Grade.Services;

namespace StudentDockerPortalProject.Grade.Controllers;

public class GradesController : Controller
{
    private readonly GradeDatabaseContext _db;
    private readonly IStudentService _studentsService;
    private readonly ILogger<GradesController> _logger;

    public GradesController(
        GradeDatabaseContext db,
        IStudentService studentsService,
        ILogger<GradesController> logger)
    {
        _db = db;
        _studentsService = studentsService;
        _logger = logger;
    }

    // Index: Lists all grades — attempts to resolve student names via HTTP
    public async Task<IActionResult> Index()
    {
        var grades = await _db.Grades
            .AsNoTracking()
            .OrderByDescending(g => g.GradeDate)
            .ToListAsync();
        var vm = new GradeIndexViewModel();
        try
        {
            var students = await _studentsService.GetAllStudentsAsync();
            var studentNames = students.ToDictionary(s => s.Id, s => $"{s.FirstName} {s.LastName}");
            vm.Grades = grades.Select(g => new GradeRowViewModel
            {
                Id = g.Id,
                StudentId = g.StudentId,
                StudentName = studentNames.GetValueOrDefault(g.StudentId, "Unknown"),
                CourseName = g.CourseName,
                Score = g.Score,
                GradeDate = g.GradeDate,
                Notes = g.Notes
            }).ToList();
        }
        catch (Exception ex)
        {
            // Gracefully degrade — show grades without student names
            _logger.LogWarning(ex, "Failed to fetch students for Grades index");

            vm.Grades = grades.Select(g => new GradeRowViewModel
            {
                Id = g.Id,
                StudentId = g.StudentId,
                CourseName = g.CourseName,
                Score = g.Score,
                GradeDate = g.GradeDate,
                Notes = g.Notes
            }).ToList();

            vm.ErrorMessage = "Unable to load student names from the student service. Some information may be unavailable.";
        }
        return View(vm);
    }

    // GET Create: Shows the grade creation form with student dropdown
    public async Task<IActionResult> Create()
    {
        var vm = new GradeFormViewModel();
        await PopulateStudentOptionsAsync(vm);
        return View(vm);
    }

    // POST Create: Validates and saves a new grade
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(GradeFormViewModel vm)
    {
        if (!ModelState.IsValid)
        {
            await PopulateStudentOptionsAsync(vm);
            return View(vm);
        }

        var grade = new Models.Grade
        {
            StudentId = vm.StudentId,
            CourseName = vm.CourseName,
            Score = vm.Score,
            GradeDate = vm.GradeDate,
            Notes = vm.Notes
        };

        _db.Grades.Add(grade);
        await _db.SaveChangesAsync();

        TempData["Success"] = "Grade added successfully.";
        return RedirectToAction(nameof(Index));
    }

    // GET Edit: Loads an existing grade into the form
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();

        var grade = await _db.Grades.FindAsync(id);
        if (grade == null) return NotFound();

        var vm = new GradeFormViewModel
        {
            Id = grade.Id,
            StudentId = grade.StudentId,
            CourseName = grade.CourseName,
            Score = grade.Score,
            GradeDate = grade.GradeDate,
            Notes = grade.Notes
        };

        await PopulateStudentOptionsAsync(vm);
        return View(vm);
    }

    // POST Edit: Updates an existing grade
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, GradeFormViewModel vm)
    {
        if (id != vm.Id) return NotFound();

        if (!ModelState.IsValid)
        {
            await PopulateStudentOptionsAsync(vm);
            return View(vm);
        }

        var grade = await _db.Grades.FindAsync(id);
        if (grade == null) return NotFound();

        grade.StudentId = vm.StudentId;
        grade.CourseName = vm.CourseName;
        grade.Score = vm.Score;
        grade.GradeDate = vm.GradeDate;
        grade.Notes = vm.Notes;

        await _db.SaveChangesAsync();

        TempData["Success"] = "Grade updated successfully.";
        return RedirectToAction(nameof(Index));
    }

    // GET Delete: Shows grade deletion confirmation with student name
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();

        var grade = await _db.Grades.FindAsync(id);
        if (grade == null) return NotFound();

        var vm = new GradeDetailViewModel
        {
            Id = grade.Id,
            StudentId = grade.StudentId,
            CourseName = grade.CourseName,
            Score = grade.Score,
            GradeDate = grade.GradeDate,
            Notes = grade.Notes
        };

        try
        {
            var student = await _studentsService.GetStudentByIdAsync(grade.StudentId)!;
            vm.StudentName = $"{student.FirstName} {student.LastName}";
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to fetch student {StudentId} for grade delete", grade.StudentId);
            vm.ErrorMessage = "Unable to load student name.";
        }

        return View(vm);
    }

    // POST Delete: Confirms and executes grade deletion
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var grade = await _db.Grades.FindAsync(id);
        if (grade != null)
        {
            _db.Grades.Remove(grade);
            await _db.SaveChangesAsync();
        }

        TempData["Success"] = "Grade deleted successfully.";
        return RedirectToAction(nameof(Index));
    }

    // Details: Shows full grade info with student name from the API
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null) return NotFound();

        var grade = await _db.Grades.AsNoTracking().FirstOrDefaultAsync(g => g.Id == id);
        if (grade == null) return NotFound();

        var vm = new GradeDetailViewModel
        {
            Id = grade.Id,
            StudentId = grade.StudentId,
            CourseName = grade.CourseName,
            Score = grade.Score,
            GradeDate = grade.GradeDate,
            Notes = grade.Notes
        };

        try
        {
            var student = await _studentsService.GetStudentByIdAsync(grade.StudentId)!;
            vm.StudentName = $"{student.FirstName} {student.LastName}";
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to fetch student {StudentId} for grade details", grade.StudentId);
            vm.ErrorMessage = "Unable to load student name.";
        }

        return View(vm);
    }

    // Helper: Populates the student dropdown from the Students API
    private async Task PopulateStudentOptionsAsync(GradeFormViewModel vm)
    {
        try
        {
            var students = await _studentsService.GetAllStudentsAsync();
            vm.StudentOptions = students
                .Select(s => new SelectListItem(
                    $"{s.FirstName} {s.LastName}",
                    s.Id.ToString()))
                .ToList();
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Failed to fetch students for grade form dropdown");
            vm.ErrorMessage = "Unable to load student list. Please try again later.";
            vm.StudentOptions = [new SelectListItem("-- Students unavailable --", "")];
        }
    }
}
