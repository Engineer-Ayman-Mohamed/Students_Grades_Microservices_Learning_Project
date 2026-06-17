// ═══════════════════════════════════════════════════════════════
// Home Controller — Landing page, Privacy, Error pages
// ═══════════════════════════════════════════════════════════════
// Serves the grade portal home page, privacy policy, and error
// page for unhandled exceptions.
// ═══════════════════════════════════════════════════════════════

using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using StudentDockerPortalProject.Grade.Models;

namespace StudentDockerPortalProject.Grade.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}