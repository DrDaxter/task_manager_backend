using taskManagerBE.Models;

namespace taskManagerBE.Interfaces;

public interface IProjectRepository
{
    public IEnumerable<Project> GetAllProjects();
    public Project? GetProjectById(int id);
    public Task<ICollection<Project>> GetProjectsByUserId(int userId);
    public Task<bool> AddProject(Project project);
    public Task<bool> UpdateProject(Project project);
    public Task<bool> DeleteProject(Project project);
    public bool ProjectExists(int id);
    public Task<bool> Save();
}