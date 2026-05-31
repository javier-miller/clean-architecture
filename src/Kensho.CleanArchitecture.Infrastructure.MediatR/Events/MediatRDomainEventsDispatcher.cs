using Kensho.CleanArchitecture.Domain.Events;
using MediatR;

namespace Kensho.CleanArchitecture.Infrastructure.MediatR.Events;

/// <summary>
/// Dispatches queued domain events through MediatR notifications.
/// </summary>
/// <seealso cref="IDomainEventsDispatcher" />
public class MediatRDomainEventsDispatcher : IDomainEventsDispatcher
{
    private readonly IDomainEventsManager _eventsManager;
    private readonly IPublisher _publisher;

    /// <summary>
    /// Initializes a new instance of the <see cref="MediatRDomainEventsDispatcher"/> class.
    /// </summary>
    /// <param name="eventsManager">The events manager.</param>
    /// <param name="publisher">The MediatR publisher.</param>
    /// <exception cref="ArgumentNullException"></exception>
    public MediatRDomainEventsDispatcher(IDomainEventsManager eventsManager, IPublisher publisher)
    {
        ArgumentNullException.ThrowIfNull(eventsManager);
        ArgumentNullException.ThrowIfNull(publisher);

        _eventsManager = eventsManager;
        _publisher = publisher;
    }

    /// <summary>
    /// Dispatches all pending domain events.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task DispatchAsync(CancellationToken cancellationToken = default)
    {
        var domainEvents = _eventsManager.GetEvents();

        while (domainEvents.TryPeek(out var domainEvent))
        {
            var notification = CreateNotification(domainEvent);

            await _publisher.Publish((object)notification, cancellationToken);

            domainEvents.Dequeue();
        }
    }

    private static INotification CreateNotification(IDomainEvent domainEvent)
    {
        var notificationType = typeof(DomainEventNotification<>).MakeGenericType(domainEvent.GetType());

        return (INotification)Activator.CreateInstance(notificationType, domainEvent)!;
    }
}
