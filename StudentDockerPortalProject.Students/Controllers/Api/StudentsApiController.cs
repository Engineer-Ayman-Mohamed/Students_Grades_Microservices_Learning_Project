// ═══════════════════════════════════════════════════════════════
// Students REST API Controller — Full CRUD over JSON
// ═══════════════════════════════════════════════════════════════
// Exposes a RESTful API at /api/students for programmatic access.
// Uses StudentDto records for request/response and includes email
// uniqueness validation. Documented via Swagger XML comments.
// ═══════════════════════════════════════════════════════════════

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentDockerPortalProject.Students.Data;
using StudentDockerPortalProject.Students.Models;
using StudentDockerPortalProject.Students.Models.Dtos;

namespace StudentDockerPortalProject.Students.Controllers.Api;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class StudentsApiController : ControllerBase
{
    private readonly StudentsDbContext _context;

    public StudentsApiController(StudentsDbContext context)
    {
        _context = context;
    }

    /// <summary>Returns all students.</summary>
    /// <response code="200">List of students retrieved successfully.</response>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<StudentDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<StudentDto>>> GetAll()
    {
        var students = await _context.Students
            .AsNoTracking()
            .OrderBy(s => s.LastName)
            .Select(s => new StudentDto(
                s.Id,
                s.FirstName,
                s.LastName,
                s.Email,
                s.DateOfBirth,
                s.EnrollmentDate
            ))
            .ToListAsync();

        return Ok(students);
    }

    /// <summary>Returns a student by ID.</summary>
    /// <param name="id">The student ID.</param>
    /// <response code="200">Student found.</response>
    /// <response code="404">Student not found.</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(StudentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<StudentDto>> GetById(int id)
    {
        var student = await _context.Students
            .AsNoTracking()
            .Where(s => s.Id == id)
            .Select(s => new StudentDto(
                s.Id,
                s.FirstName,
                s.LastName,
                s.Email,
                s.DateOfBirth,
                s.EnrollmentDate
            ))
            .FirstOrDefaultAsync();

        if (student is null) return NotFound(new { message = $"Student with ID {id} not found." });

        return Ok(student);
    }

    /// <summary>Creates a new student.</summary>
    /// <param name="request">Student details.</param>
    /// <response code="201">Student created successfully.</response>
    /// <response code="400">Validation failed.</response>
    [HttpPost]
    [ProducesResponseType(typeof(StudentDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<StudentDto>> Create([FromBody] CreateStudentRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var emailExists = await _context.Students.AnyAsync(s => s.Email == request.Email);
        if (emailExists)
            return Conflict(new { message = "A student with this email already exists." });

        var student = new Student
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            DateOfBirth = request.DateOfBirth,
            EnrollmentDate = request.EnrollmentDate
        };

        _context.Students.Add(student);
        await _context.SaveChangesAsync();

        var dto = new StudentDto(
            student.Id,
            student.FirstName,
            student.LastName,
            student.Email,
            student.DateOfBirth,
            student.EnrollmentDate
        );

        return CreatedAtAction(nameof(GetById), new { id = student.Id }, dto);
    }

    /// <summary>Updates an existing student.</summary>
    /// <param name="id">The student ID.</param>
    /// <param name="request">Updated student details.</param>
    /// <response code="200">Student updated successfully.</response>
    /// <response code="400">Validation failed.</response>
    /// <response code="404">Student not found.</response>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(StudentDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<StudentDto>> Update(int id, [FromBody] UpdateStudentRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var student = await _context.Students.FindAsync(id);
        if (student is null) return NotFound(new { message = $"Student with ID {id} not found." });

        var emailExists = await _context.Students.AnyAsync(s => s.Email == request.Email && s.Id != id);
        if (emailExists)
            return Conflict(new { message = "A student with this email already exists." });

        student.FirstName = request.FirstName;
        student.LastName = request.LastName;
        student.Email = request.Email;
        student.DateOfBirth = request.DateOfBirth;
        student.EnrollmentDate = request.EnrollmentDate;

        await _context.SaveChangesAsync();

        var dto = new StudentDto(
            student.Id,
            student.FirstName,
            student.LastName,
            student.Email,
            student.DateOfBirth,
            student.EnrollmentDate
        );

        return Ok(dto);
    }

    /// <summary>Deletes a student by ID.</summary>
    /// <param name="id">The student ID.</param>
    /// <response code="204">Student deleted successfully.</response>
    /// <response code="404">Student not found.</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var student = await _context.Students.FindAsync(id);
        if (student is null) return NotFound(new { message = $"Student with ID {id} not found." });

        _context.Students.Remove(student);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
