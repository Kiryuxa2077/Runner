using RunnerScore.Models;

namespace RunnerScore.Interfaces;

public interface IUserRepository : IRepository<User, string>
{
    public Task<User> GetByNameAsync(string name);
}
