using Kensho.CleanArchitecture.Application.Common.Persistence;
using Kensho.CleanArchitecture.Domain;

namespace Kensho.CleanArchitecture.Infrastructure.Persistence;

/// <summary>
/// Specification Factory
/// </summary>
/// <seealso cref="ISpecificationFactory" />
internal class SpecificationFactory : ISpecificationFactory
{

    /// <summary>
    /// Gets the builder.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <returns></returns>
    public ISpecificationBuilder<TEntity> GetBuilder<TEntity>() where TEntity : Entity =>
            Specification<TEntity>.Create();
}
