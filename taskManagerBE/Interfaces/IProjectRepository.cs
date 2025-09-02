using taskManagerBE.Models;

namespace taskManagerBE.Interfaces;

public interface IProjectRepository
{
    public ICollection<Project> GetAllProjects();
    public Project? GetProjectById(int id);
    
    public bool AddProject(Project project);
}