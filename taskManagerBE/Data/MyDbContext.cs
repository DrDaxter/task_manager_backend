using Microsoft.EntityFrameworkCore;
using taskManagerBE.Models;
using Task = taskManagerBE.Models.Task;

namespace taskManagerBE.Data;

public class MyDbContext : DbContext
{
    public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }
    
    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<Task> Tasks { get; set; }

    public virtual DbSet<User> Users { get; set; }
    
}
