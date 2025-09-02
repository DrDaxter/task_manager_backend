using System.ComponentModel.DataAnnotations;

namespace taskManagerBE.Dto;

public class ProjectDto
{

    [Required]
    public string ProjectName { get; set; } = null!;

    [Required]
    public string? Description { get; set; }

    [Required]
    public int UserId { get; set; }
}