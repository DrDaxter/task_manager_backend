using taskManagerBE.Models;

namespace taskManagerBE.Interfaces;

public interface IProjectRepository
{
    public ICollection<Project> GetAllProjects();
    public Project? GetProjectById(int id);
    
    public bool AddProject(Project project);
    public bool UpdateProject(Project project);
    public bool DeleteProject(Project project);
    public bool ProjectExists(int id);
    public bool Save();
}