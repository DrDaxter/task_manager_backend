using System.ComponentModel.DataAnnotations;

namespace taskManagerBE.Dto;

public class CreateProjectDto
{
    [Required]
    public required string ProjectName { get; set; }
    
    [Required]
    public string? Description { get; set; }
    
    [Required]
    public int UserId { get; set; }
}