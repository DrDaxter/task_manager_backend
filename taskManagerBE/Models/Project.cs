using System;
using System.Collections.Generic;

namespace taskManagerBE.Models;

public class Project
{
    public int Id { get; set; }

    public string ProjectName { get; set; } = null!;

    public string? Description { get; set; }

    public int UserId { get; set; }

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();

    public virtual User User { get; set; } = null!;
}
