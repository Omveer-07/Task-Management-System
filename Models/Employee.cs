using System.ComponentModel.DataAnnotations;

namespace TaskManagementSystem.Models;

public class Employee
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = string.Empty;

    [Required]
    public string Email { get; set; } = string.Empty;

    // Link to AspNetUsers
    public string? IdentityUserId { get; set; }

    public ICollection<TaskItem>? Tasks { get; set; }
}