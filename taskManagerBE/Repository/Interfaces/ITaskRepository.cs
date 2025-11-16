using taskManagerBE.Models;
using Task = taskManagerBE.Models.Task;

namespace taskManagerBE.Interfaces;

public interface ITaskRepository
{
    public Task<ICollection<Task>> GetTasksByProjectId(int projectId);
    public bool AddTask(Task task);
    public bool Save();
}