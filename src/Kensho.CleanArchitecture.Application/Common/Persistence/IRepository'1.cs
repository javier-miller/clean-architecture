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
    ValueTask<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken = default);

    ValueTask<TEntity> UpdateAsync(TEntity entity);

    ValueTask RemoveAsync(TEntity entity);
}