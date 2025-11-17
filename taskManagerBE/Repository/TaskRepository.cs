using Microsoft.EntityFrameworkCore;
using taskManagerBE.Data;
using taskManagerBE.Interfaces;
using Task = taskManagerBE.Models.Task;

namespace taskManagerBE.Repository;

public class TaskRepository: ITaskRepository
{
    private readonly MyDbContext _context;

    public TaskRepository(MyDbContext context)
    {
        _context = context;
    }
    
    public async Task<ICollection<Task>> GetTasksByProjectId(int projectId)
    {
        return await _context.Tasks.Where(t => t.ProjectId == projectId).ToListAsync();
    }

    public bool AddTask(Task task)
    {
        task.State = true;
        _context.Tasks.Add(task);
        return Save();
    }

    public bool Save()
    {
        return _context.SaveChanges() >= 0;
    }
}