namespace Kensho.CleanArchitecture.Domain.Events;

/// <summary>
/// Domain Events Manager Interface
/// </summary>
public interface IDomainEventsManager
{
    /// <summary>
    /// Subscribes the events.
    /// </summary>
    /// <param name="events">The events.</param>
    void SubscribeEvents(IEnumerable<IDomainEvent> events);

    /// <summary>
    /// Gets the events.
    /// </summary>
    /// <returns></returns>
    Queue<IDomainEvent> GetEvents();
}
