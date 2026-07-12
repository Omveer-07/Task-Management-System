using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Data;
using TaskManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;

namespace TaskManagementSystem.Controllers;
[Authorize(Roles = "Admin")]
public class DepartmentController : Controller
{
    private readonly ApplicationDbContext _context;

    public DepartmentController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var departments = _context.Departments.ToList();
        return View(departments);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Department department)
    {
        _context.Departments.Add(department);
        _context.SaveChanges();

        return RedirectToAction(nameof(Index));
    }

    public IActionResult Edit(int id)
    {
        var department = _context.Departments.Find(id);

        if (department == null)
        {
            return NotFound();
        }

        return View(department);
    }

    [HttpPost]
    public IActionResult Edit(Department department)
    {
        _context.Departments.Update(department);
        _context.SaveChanges();

        return RedirectToAction(nameof(Index));
    }

    public IActionResult Delete(int id)
    {
        var department = _context.Departments.Find(id);

        if (department == null)
        {
            return NotFound();
        }

        _context.Departments.Remove(department);
        _context.SaveChanges();

        return RedirectToAction(nameof(Index));
    }
}