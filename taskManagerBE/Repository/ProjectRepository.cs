using taskManagerBE.Data;
using taskManagerBE.Interfaces;
using taskManagerBE.Models;

namespace taskManagerBE.Repository;

public class ProjectRepository : IProjectRepository
{
    private readonly MyDbContext _context;
    public ProjectRepository(MyDbContext context)
    {
        _context = context;
    }
    
    public ICollection<Project> GetAllProjects()
    {
        return _context.Projects.ToList();
    }

    public Project? GetProjectById(int id)
    {
        var result =  _context.Projects.Find(id);
        
        return result;
    }

    public bool AddProject(Project project)
    {
        _context.Projects.Add(project);
        var result = _context.SaveChanges() > 0;
        return result;
    }
    
}