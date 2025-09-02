using System;
using System.Collections.Generic;

namespace taskManagerBE.Models;

public class Task
{
    public int Id { get; set; }

    public string TaskName { get; set; } = null!;

    public string? Description { get; set; }

    public bool State { get; set; }

    public string PriorityType { get; set; } = null!;

    public DateTime? LimitDate { get; set; }

    public int UserId { get; set; }

    public int ProjectId { get; set; }

    public virtual Project Project { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
