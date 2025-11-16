using taskManagerBE.Models;

namespace taskManagerBE.Interfaces;

public interface IProjectRepository
{
    public IEnumerable<Project> GetAllProjects();
    public Project? GetProjectById(int id);
    public Task<ICollection<Project>> GetProjectsByUserId(int userId);
    public bool AddProject(Project project);
    public bool UpdateProject(Project project);
    public bool DeleteProject(Project project);
    public bool ProjectExists(int id);
    public bool Save();
}