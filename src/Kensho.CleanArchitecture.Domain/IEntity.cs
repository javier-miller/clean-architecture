using Kensho.CleanArchitecture.Domain.Events;

namespace Kensho.CleanArchitecture.Domain;

/// <summary>
/// Entity interface
/// </summary>
public interface IEntity
{
    /// <summary>
    /// Pops the domain events.
    /// </summary>
    /// <returns></returns>
    IEnumerable<IDomainEvent> PopDomainEvents();
}

