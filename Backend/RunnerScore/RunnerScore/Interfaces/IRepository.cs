using System.Linq.Expressions;

namespace RunnerScore.Interfaces;

public interface IRepository<TEntity, IndexType> 
    where TEntity : class, IIdentifiable<IndexType>
{
    Task AddAsync(TEntity entity);
    Task AddRangeAsync(IEnumerable<TEntity> entities);
    Task UpdateAsync(TEntity entity);
    Task<TEntity> GetAsync(IndexType id, Expression<Func<TEntity, bool>> predication = null);
}
