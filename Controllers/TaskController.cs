using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Data;
using TaskManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace TaskManagementSystem.Controllers;

public class TaskController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;

    public TaskController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    [Authorize]
    public IActionResult Index()
    {
        // If Admin, show all tasks
        if (User.IsInRole("Admin"))
        {
            var tasks = _context.Tasks
                .Include(t => t.Project)
                .Include(t => t.Employee)
                .ToList();

            return View(tasks);
        }

        // Employee: get logged-in user's Identity Id
        var identityUserId = _userManager.GetUserId(User);

        // Find the employee linked to this Identity user
        var employee = _context.Employees
            .FirstOrDefault(e => e.IdentityUserId == identityUserId);

        if (employee == null)
        {
            return View(new List<TaskItem>());
        }

        // Show only tasks assigned to this employee
        var myTasks = _context.Tasks
            .Include(t => t.Project)
            .Include(t => t.Employee)
            .Where(t => t.EmployeeId == employee.Id)
            .ToList();

        return View(myTasks);
    }

    [Authorize(Roles = "Admin")]
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

    [Authorize(Roles = "Admin")]
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

    [Authorize(Roles = "Admin")]
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