using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Data;
using TaskManagementSystem.Models;
using TaskManagementSystem.ViewModels;

namespace TaskManagementSystem.Controllers;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _context;

    public HomeController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var model = new DashboardViewModel
        {
            TotalDepartments = _context.Departments.Count(),
            TotalProjects = _context.Projects.Count(),
            TotalTasks = _context.Tasks.Count(),
            PendingTasks = _context.Tasks.Count(t => t.Status == "Pending"),
            CompletedTasks = _context.Tasks.Count(t => t.Status == "Completed")
        };

        return View(model);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
        });
    }
}