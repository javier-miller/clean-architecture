using Kensho.CleanArchitecture.Domain;
using System.Linq.Expressions;

namespace Kensho.CleanArchitecture.Application.Common.Persistence;

/// <summary>
/// Read Repository Repository
/// </summary>
/// <typeparam name="TEntity">The type of the entity.</typeparam>
/// <seealso cref="IRepository" />
public interface IReadRepository<TEntity> : IRepository
    where TEntity : Entity
{
    /// <summary>
    /// Gets all asynchronous.
    /// </summary>
    /// <returns></returns>
    ValueTask<IEnumerable<TEntity>> GetAllAsync();

    /// <summary>
    /// Gets all asynchronous.
    /// </summary>
    /// <param name="specification">The specification.</param>
    /// <returns></returns>
    ValueTask<IEnumerable<TEntity>> GetAllAsync(ISpecification<TEntity> specification);

    /// <summary>
    /// Gets all asynchronous.
    /// </summary>
    /// <param name="predicate">The predicate.</param>
    /// <returns></returns>
    ValueTask<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// Gets the first or default asynchronous.
    /// </summary>
    /// <param name="predicate">The predicate.</param>
    /// <returns></returns>
    ValueTask<TEntity?> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// Gets the single asynchronous.
    /// </summary>
    /// <param name="specification">The specification.</param>
    /// <returns></returns>
    ValueTask<TEntity> GetSingleAsync(ISpecification<TEntity> specification);

    /// <summary>
    /// Gets the single asynchronous.
    /// </summary>
    /// <param name="predicate">The predicate.</param>
    /// <returns></returns>
    ValueTask<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// Gets the by identifier asynchronous.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <returns></returns>
    ValueTask<TEntity?> GetByIdAsync(object id);
}
