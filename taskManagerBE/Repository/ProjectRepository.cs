using Microsoft.EntityFrameworkCore;
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
    
    public IEnumerable<Project> GetAllProjects()
    {
        return _context.Projects.ToList();
    }

    public Project? GetProjectById(int id)
    {
        var result =  _context.Projects.Find(id);
        
        return result;
    }

    public async Task<ICollection<Project>> GetProjectsByUserId(int userId)
    {
        if (userId < 0) return [];
        
        return await _context.Projects.Where(p => p.UserId == userId).ToListAsync();
    }

    public bool AddProject(Project project)
    {
        _context.Projects.Add(project);
        return Save();
    }

    public bool UpdateProject(Project project)
    {
        _context.Projects.Update(project);
        return Save();
    }

    public bool DeleteProject(Project project)
    {
        _context.Remove(project);
        return Save();
    }

    public bool ProjectExists(int id)
    {
        return _context.Projects.Any(e => e.Id == id);
    }

    public bool Save()
    {
        return _context.SaveChanges() > 0;
    }
}