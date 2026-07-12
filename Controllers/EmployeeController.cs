using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Data;
using TaskManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;

namespace TaskManagementSystem.Controllers;

[Authorize(Roles = "Admin")]
public class EmployeeController : Controller
{
    private readonly ApplicationDbContext _context;

    public EmployeeController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var employees = _context.Employees.ToList();
        return View(employees);
    }

    public IActionResult Edit(int id)
    {
        var employee = _context.Employees.Find(id);

        if (employee == null)
            return NotFound();

        return View(employee);
    }

    [HttpPost]
    public IActionResult Edit(Employee employee)
    {
        if (ModelState.IsValid)
        {
            _context.Employees.Update(employee);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        return View(employee);
    }

    public IActionResult Delete(int id)
    {
        var employee = _context.Employees.Find(id);

        if (employee == null)
            return NotFound();

        _context.Employees.Remove(employee);
        _context.SaveChanges();

        return RedirectToAction(nameof(Index));
    }
}