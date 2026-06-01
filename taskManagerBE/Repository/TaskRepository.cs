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

    public async Task<bool> AddTask(Task task)
    {
        task.State = true;
        await _context.Tasks.AddAsync(task);
        return await SaveAsync();
    }

    public async Task<bool> SaveAsync()
    {
        var result = await _context.SaveChangesAsync();
        return result >= 0;
    }
}