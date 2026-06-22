using System.ComponentModel.DataAnnotations;

namespace TaskManagementSystem.Models;

public class Project
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public int DepartmentId { get; set; }

    public Department? Department { get; set; }
}