using taskManagerBE.Models;

namespace taskManagerBE.Interfaces;

public interface IUserRepository
{
    public  Task<bool> UserExists(int userId);
}