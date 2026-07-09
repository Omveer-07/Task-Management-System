using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Data;
using TaskManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;

namespace TaskManagementSystem.Controllers;
[Authorize]
public class ProjectController : Controller
{
    private readonly ApplicationDbContext _context;

    public ProjectController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var projects = _context.Projects
            .Include(p => p.Department)
            .ToList();

        return View(projects);
    }

    public IActionResult Create()
    {
        ViewBag.Departments = new SelectList(
            _context.Departments,
            "Id",
            "Name");

        return View();
    }

    [HttpPost]
    public IActionResult Create(Project project)
    {
        _context.Projects.Add(project);
        _context.SaveChanges();

        return RedirectToAction(nameof(Index));
    }
}