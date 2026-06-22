using System.ComponentModel.DataAnnotations;

namespace TaskManagementSystem.Models;

public class TaskItem
{
    public int Id { get; set; }

    [Required]
    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public DateTime DueDate { get; set; }

    public string Status { get; set; } = "Pending";

    public int ProjectId { get; set; }

    public Project? Project { get; set; }
}