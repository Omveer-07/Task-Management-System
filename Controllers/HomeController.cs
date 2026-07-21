using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Data;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;

    public HomeController(
        ApplicationDbContext context,
        UserManager<IdentityUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    [Authorize]
    public IActionResult Index()
    {
        if (User.IsInRole("Admin"))
        {
            ViewBag.TotalProjects = _context.Projects.Count();
            ViewBag.TotalEmployees = _context.Employees.Count();
            ViewBag.TotalTasks = _context.Tasks.Count();

            ViewBag.PendingTasks =
                _context.Tasks.Count(t => t.Status == "Pending");

            ViewBag.InProgressTasks =
                _context.Tasks.Count(t => t.Status == "In Progress");

            ViewBag.CompletedTasks =
                _context.Tasks.Count(t => t.Status == "Completed");
        }
        else
        {
            var userId = _userManager.GetUserId(User);

            var employee = _context.Employees
                .FirstOrDefault(e => e.IdentityUserId == userId);

            if (employee != null)
            {
                ViewBag.MyTasks =
                    _context.Tasks.Count(t => t.EmployeeId == employee.Id);

                ViewBag.MyPending =
                    _context.Tasks.Count(t =>
                        t.EmployeeId == employee.Id &&
                        t.Status == "Pending");

                ViewBag.MyProgress =
                    _context.Tasks.Count(t =>
                        t.EmployeeId == employee.Id &&
                        t.Status == "In Progress");

                ViewBag.MyCompleted =
                    _context.Tasks.Count(t =>
                        t.EmployeeId == employee.Id &&
                        t.Status == "Completed");

                ViewBag.Upcoming =
                    _context.Tasks.Count(t =>
                        t.EmployeeId == employee.Id &&
                        t.DueDate >= DateTime.Today &&
                        t.Status != "Completed");
            }
        }

        return View();
    }
}