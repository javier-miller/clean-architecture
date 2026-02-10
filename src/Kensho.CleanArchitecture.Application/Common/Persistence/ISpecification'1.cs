using Kensho.CleanArchitecture.Domain;

namespace Kensho.CleanArchitecture.Application.Common.Persistence;

/// <summary>
/// Specification Interface
/// </summary>
/// <typeparam name="TEntity">The type of the entity.</typeparam>
public interface ISpecification<TEntity> where TEntity : Entity
{
    /// <summary>
    /// Applies the specified query.
    /// </summary>
    /// <param name="query">The query.</param>
    /// <returns></returns>
    IQueryable<TEntity> Apply(IQueryable<TEntity> query);
}

