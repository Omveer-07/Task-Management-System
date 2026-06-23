namespace TaskManagementSystem.ViewModels;

public class DashboardViewModel
{
    public int TotalDepartments { get; set; }

    public int TotalProjects { get; set; }

    public int TotalTasks { get; set; }

    public int PendingTasks { get; set; }

    public int CompletedTasks { get; set; }
}