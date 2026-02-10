using Kensho.CleanArchitecture.Domain.Events;

namespace Kensho.CleanArchitecture.Domain;

/// <summary>
/// Entity
/// </summary>
/// <seealso cref="Equatable&lt;Entity&gt;" />
/// <seealso cref="IEntity" />
public abstract class Entity : Equatable<Entity>, IEntity
{
    protected readonly List<IDomainEvent> _domainEvents = [];

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    protected Entity() { }

    /// <summary>
    /// Pops the domain events.
    /// </summary>
    /// <returns></returns>
    public IEnumerable<IDomainEvent> PopDomainEvents()
    {
        var result = new List<IDomainEvent>(_domainEvents);
        _domainEvents.Clear();

        return result;
    }

    /// <summary>
    /// Adds the event.
    /// </summary>
    /// <typeparam name="TEvent">The type of the event.</typeparam>
    /// <param name="domainEvent">The domain event.</param>
    protected void AddEvent<TEvent>(TEvent domainEvent) where TEvent : IDomainEvent
    {
        if (!_domainEvents.Any(e=>e.GetType() == typeof(TEvent)))
            _domainEvents.Add(domainEvent);
    }
}
