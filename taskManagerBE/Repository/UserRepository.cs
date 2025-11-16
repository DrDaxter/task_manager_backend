using Microsoft.EntityFrameworkCore;
using taskManagerBE.Data;
using taskManagerBE.Interfaces;
using taskManagerBE.Models;

namespace taskManagerBE.Repository;

public class UserRepository : IUserRepository
{
    private readonly MyDbContext _context;

    public UserRepository(MyDbContext context)
    {
        _context = context;
    }


    public async Task<bool> UserExists(int userId)
    {
        var userExist = await _context.Users.AnyAsync(u => u.Id == userId);
        return userExist;
    }
}