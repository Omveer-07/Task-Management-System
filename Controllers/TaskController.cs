using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Data;
using TaskManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;

namespace TaskManagementSystem.Controllers;
[Authorize]
public class TaskController : Controller
{
    private readonly ApplicationDbContext _context;

    public TaskController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var tasks = _context.Tasks
            .Include(t => t.Project)
            .Include(t => t.Employee)
            .ToList();

        return View(tasks);
    }

    public IActionResult Create()
    {
        ViewBag.Projects = new SelectList(
            _context.Projects,
            "Id",
            "Name");

        ViewBag.Employees = new SelectList(
            _context.Employees,
            "Id",
            "Name");

        return View();
    }

    [HttpPost]
    public IActionResult Create(TaskItem task)
    {
        _context.Tasks.Add(task);
        _context.SaveChanges();

        return RedirectToAction(nameof(Index));
    }

    public IActionResult Edit(int id)
    {
        var task = _context.Tasks.Find(id);

        if (task == null)
        {
            return NotFound();
        }

        ViewBag.Projects = new SelectList(
            _context.Projects,
            "Id",
            "Name",
            task.ProjectId);

        ViewBag.Employees = new SelectList(
            _context.Employees,
            "Id",
            "Name",
            task.EmployeeId);

        return View(task);
    }

    [HttpPost]
    public IActionResult Edit(TaskItem task)
    {
        _context.Tasks.Update(task);
        _context.SaveChanges();

        return RedirectToAction(nameof(Index));
    }

    public IActionResult Delete(int id)
    {
        var task = _context.Tasks.Find(id);

        if (task == null)
        {
            return NotFound();
        }

        _context.Tasks.Remove(task);
        _context.SaveChanges();

        return RedirectToAction(nameof(Index));
    }
}