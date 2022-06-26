using MongoDB.Driver;
using RunnerScore.Interfaces;
using System.Linq.Expressions;

namespace RunnerScore.Repositories;

public class Repository<TEntity, IndexType> : IRepository<TEntity, IndexType>
    where TEntity : class, IIdentifiable<IndexType>
{
    private readonly ApplicationContext<TEntity> _context;

    protected IMongoCollection<TEntity> DbSet;

    public Repository(ApplicationContext<TEntity> context)
    {
        _context = context;

        DbSet = _context.GetCollection<TEntity>(typeof(TEntity).Name);
    }

    public async Task AddAsync(TEntity entity)
    {
        await DbSet.InsertOneAsync(entity);
    }

    public async Task AddRangeAsync(IEnumerable<TEntity> entities)
    {
        await DbSet.InsertManyAsync(entities);
    }

    public async Task UpdateAsync(TEntity entity)
    {
        await DbSet.ReplaceOneAsync(x => Equals(x.Id, entity.Id), entity);
    }

    public async Task<TEntity> GetAsync(IndexType id, Expression<Func<TEntity, bool>> predication = null)
    {
        Expression<Func<TEntity, bool>> filter;

        Expression<Func<TEntity, bool>> rightPredication = x => Equals(x.Id, id);

        if (predication != null)
        {
            var bodyExpression = Expression.AndAlso(predication.Body, rightPredication.Body);

            filter = Expression.Lambda<Func<TEntity, bool>>(bodyExpression, predication.Parameters[0]);
        }
        else
        {
            filter = rightPredication;
        }

        return await DbSet.Find(filter).FirstOrDefaultAsync();
    }
}
