using Microsoft.EntityFrameworkCore;
using Kensho.CleanArchitecture.Application.Common.Persistence;
using Kensho.CleanArchitecture.Domain;
using System.Linq.Expressions;

namespace Kensho.CleanArchitecture.Infrastructure.Persistence;

/// <summary>
/// Repository
/// </summary>
/// <typeparam name="TDbContext">The type of the database context.</typeparam>
/// <typeparam name="TEntity">The type of the entity.</typeparam>
/// <typeparam name="TKey">The type of the key.</typeparam>
/// <seealso cref="IRepository&lt;TEntity, TKey&gt;" />
public class Repository<TDbContext, TEntity> : IRepository<TEntity>
    where TDbContext : DbContext
    where TEntity : Entity
{
    protected readonly RepositoryOptions _options;
    protected readonly TDbContext _dbContext;
    protected readonly DbSet<TEntity> _dbSet;

    /// <summary>
    /// Initializes a new instance of the <see cref="Repository{TDbContext, TEntity, TKey}"/> class.
    /// </summary>
    /// <param name="dbContext">The database context.</param>
    /// <param name="options">The options.</param>
    /// <exception cref="System.ArgumentNullException"></exception>
    public Repository(TDbContext dbContext, RepositoryOptions options)
    {
        ArgumentNullException.ThrowIfNull(dbContext);
        ArgumentNullException.ThrowIfNull(options);

        _options = options;
        _dbContext = dbContext;
        _dbSet = dbContext.Set<TEntity>();
    }

    /// <summary>
    /// Gets all asynchronous.
    /// </summary>
    /// <returns></returns>
    public virtual async ValueTask<IEnumerable<TEntity>> GetAllAsync()
    {
        var query = FormatQuery(_dbSet);

        var result = await query.ToListAsync();

        return result;
    }

    /// <summary>
    /// Gets all asynchronous.
    /// </summary>
    /// <param name="specification">The specification.</param>
    /// <returns></returns>
    public virtual async ValueTask<IEnumerable<TEntity>> GetAllAsync(ISpecification<TEntity> specification)
    {
        var query = specification.Apply(_dbSet);
        query = FormatQuery(query);

        var result = await query.ToListAsync();

        return result;
    }

    /// <summary>
    /// Gets all asynchronous.
    /// </summary>
    /// <param name="predicate">The predicate.</param>
    /// <returns></returns>
    public virtual async ValueTask<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate)
    {
        var query = _dbSet.Where(predicate);
        var result = await FormatQuery(query).ToListAsync();

        return result;
    }

    /// <summary>
    /// Gets the single asynchronous.
    /// </summary>
    /// <param name="specification">The specification.</param>
    /// <returns></returns>
    public virtual async ValueTask<TEntity> GetSingleAsync(ISpecification<TEntity> specification)
    {
        var query = specification.Apply(_dbSet);
        query = FormatQuery(query);

        var result = await query.SingleAsync();

        return result;
    }

    /// <summary>
    /// Gets the first or default asynchronous.
    /// </summary>
    /// <param name="predicate">The predicate.</param>
    /// <returns></returns>
    public virtual async ValueTask<TEntity?> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
    {
        var query = _dbSet.Where(predicate);

        var result = await FormatQuery(query).FirstOrDefaultAsync();

        return result;
    }

    /// <summary>
    /// Gets the single asynchronous.
    /// </summary>
    /// <param name="predicate">The predicate.</param>
    /// <returns></returns>
    public virtual async ValueTask<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> predicate)
    {
        var query = _dbSet.Where(predicate);

        var result = await FormatQuery(query).SingleAsync();

        return result;
    }

    /// <summary>
    /// Gets the by identifier asynchronous.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <returns></returns>
    public virtual async ValueTask<TEntity?> GetByIdAsync(object id)
    {
        if (_options.AsNoTracking) _dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

        var result = await _dbSet.FindAsync(id);
        _dbContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.TrackAll;

        return result;
    }

    /// <summary>
    /// Creates the asynchronous.
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <returns></returns>
    public virtual async ValueTask<TEntity> CreateAsync(TEntity entity)
    {
        var result = await _dbSet.AddAsync(entity);

        return result.Entity;
    }

    /// <summary>
    /// Updates the asynchronous.
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <returns></returns>
    public virtual ValueTask<TEntity> UpdateAsync(TEntity entity)
    {
        _dbSet.Attach(entity);
        var entry = _dbSet.Entry(entity);
        entry.State = EntityState.Modified;

        return ValueTask.FromResult(entry.Entity);
    }

    /// <summary>
    /// Removes the asynchronous.
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <returns></returns>
    public virtual ValueTask RemoveAsync(TEntity entity)
    {
        _dbSet.Remove(entity);
        return ValueTask.CompletedTask;
    }

    /// <summary>
    /// Formats the query.
    /// </summary>
    /// <param name="query">The query.</param>
    /// <returns></returns>
    protected virtual IQueryable<TEntity> FormatQuery(IQueryable<TEntity> query)
    {
        if (_options.AsNoTracking)
            return query.AsNoTracking();

        return query;
    }
}
