using System.ComponentModel.DataAnnotations;

namespace taskManagerBE.Dto;

public class CreateTaskDto
{
    [Required]
    public string TaskName { get; set; } = null!;

    public string? Description { get; set; }

    public string PriorityType { get; set; } = null!;

    public DateTime? LimitDate { get; set; }
    
    [Required]
    public int UserId { get; set; }

    [Required]
    public int ProjectId { get; set; }
}