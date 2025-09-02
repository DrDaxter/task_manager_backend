using System;
using System.Collections.Generic;

namespace taskManagerBE.Models;

public class User
{
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    public string PasswordHas { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Rol { get; set; } = null!;

    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
}
