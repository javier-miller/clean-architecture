using Kensho.CleanArchitecture.Domain;

namespace Kensho.CleanArchitecture.Application.Common.Persistence;

/// <summary>
/// Specification Factory Interface
/// </summary>
public interface ISpecificationFactory
{
    /// <summary>
    /// Gets the builder.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <returns></returns>
    ISpecificationBuilder<TEntity> GetBuilder<TEntity>() where TEntity : Entity;
}
