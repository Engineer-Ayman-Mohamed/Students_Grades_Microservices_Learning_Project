using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentDockerPortalProject.Students.Data;
using StudentDockerPortalProject.Students.Models;

namespace StudentDockerPortalProject.Students.Controllers;

public class StudentsController : Controller
{
    private readonly StudentsDbContext _context;
    public StudentsController(StudentsDbContext context)
    {
        _context = context;
    }
    public async Task<ActionResult<List<Student>>> Index()
    {
        List<Student> students = await _context.Students.AsNoTracking().ToListAsync();
        return View(students);
    }
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Student student)
    {
        if (ModelState.IsValid)
        {
            _context.Add(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(student);
    }
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();
        Student? student = await _context.Students.FindAsync(id);
        if (student == null) return NotFound();
        return View(student);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Student student)
    {
        if (id != student.Id) return NotFound();
        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(student);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Students.Any(s => s.Id == id)) return NotFound();
                throw;
            }
            return RedirectToAction(nameof(Index));
        }
        return View(student);
    }
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null) return NotFound();
        var student = await _context.Students.FindAsync(id);
        if (student == null) return NotFound();
        return View(student);
    }
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var student = await _context.Students.FindAsync(id);
        if (student != null) _context.Students.Remove(student);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    public async Task<ActionResult<Student>> Details(int? id)
    {
        if (id == null) return NotFound();
        var student = await _context.Students.FindAsync(id);
        if (student == null) return NotFound();
        return View(student);
    }
    public async Task<ActionResult<List<Student>>> GetAll()
    {
        var students = await  _context.Students.AsNoTracking().ToListAsync();
        return Json(students);
    }
    public async Task<ActionResult<Student>> GetById(int? id)
    {
        if (id == null) return NotFound();
        var student = await _context.Students.FindAsync(id);
        if (student == null) return NotFound();
        return Json(student);
    }
}