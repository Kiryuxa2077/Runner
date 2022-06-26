using MongoDB.Driver;
using RunnerScore.Interfaces;
using RunnerScore.Models;

namespace RunnerScore.Repositories;

public class UserRepository : Repository<User, string>, IUserRepository
{
    private readonly ApplicationContext<User> _context;

    public UserRepository(ApplicationContext<User> context)
        : base(context)
    {
        _context = context;
    }

    public async Task<User> GetByNameAsync(string name)
    {
        return await DbSet.Find(x => x.Name == name).FirstOrDefaultAsync();
    }
}
