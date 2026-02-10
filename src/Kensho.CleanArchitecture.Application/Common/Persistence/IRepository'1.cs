using Kensho.CleanArchitecture.Domain;

namespace Kensho.CleanArchitecture.Application.Common.Persistence;

/// <summary>
/// Repository Interface
/// </summary>
/// <typeparam name="TEntity">The type of the entity.</typeparam>
/// <seealso cref="IReadRepository&lt;TEntity&gt;" />
public interface IRepository<TEntity> : IReadRepository<TEntity>
    where TEntity : Entity
{
    /// <summary>
    /// Creates the asynchronous.
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <returns></returns>
    ValueTask<TEntity> CreateAsync(TEntity entity);

    /// <summary>
    /// Updates the asynchronous.
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <returns></returns>
    ValueTask<TEntity> UpdateAsync(TEntity entity);

    /// <summary>
    /// Removes the asynchronous.
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <returns></returns>
    ValueTask RemoveAsync(TEntity entity);
}