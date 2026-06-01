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

    public async Task<bool> AddProject(Project project)
    {
        await _context.Projects.AddAsync(project);
        return await Save();
    }

    public async Task<bool> UpdateProject(Project project)
    {
        _context.Projects.Update(project);
        return await Save();
    }

    public async Task<bool> DeleteProject(Project project)
    {
        _context.Remove(project);
        return await Save();
    }

    public bool ProjectExists(int id)
    {
        return _context.Projects.Any(e => e.Id == id);
    }

    public async Task<bool> Save()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}