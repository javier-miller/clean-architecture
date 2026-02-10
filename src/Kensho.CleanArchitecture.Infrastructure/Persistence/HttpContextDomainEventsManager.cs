using Microsoft.AspNetCore.Http;
using Kensho.CleanArchitecture.Domain.Events;

namespace Kensho.CleanArchitecture.Infrastructure.Persistence;

/// <summary>
/// Http Context Domain Events Manager
/// </summary>
/// <seealso cref="IDomainEventsManager" />
public class HttpContextDomainEventsManager : IDomainEventsManager
{
    private static readonly Queue<IDomainEvent> _empty = new Queue<IDomainEvent>();

    private readonly IHttpContextAccessor _httpContextAccessor;
    private const string QUEUE_NAME = "DomainEventsQueue";

    /// <summary>
    /// Initializes a new instance of the <see cref="HttpContextDomainEventsManager"/> class.
    /// </summary>
    /// <param name="httpContextAccessor">The HTTP context accessor.</param>
    /// <exception cref="ArgumentNullException"></exception>
    public HttpContextDomainEventsManager(IHttpContextAccessor httpContextAccessor)
    {
        ArgumentNullException.ThrowIfNull(httpContextAccessor);

        _httpContextAccessor = httpContextAccessor;
    }

    /// <summary>
    /// Subscribes the events.
    /// </summary>
    /// <param name="events">The events.</param>
    public void SubscribeEvents(IEnumerable<IDomainEvent> events)
    {
        if (_httpContextAccessor?.HttpContext is null) return;

        var domainEventsQueue = _httpContextAccessor.HttpContext.Items.TryGetValue(QUEUE_NAME, out var value)
            && value is Queue<IDomainEvent> existingDomainEvents
            ? existingDomainEvents : new Queue<IDomainEvent>();

        foreach (var domainEvent in events)
        {
            domainEventsQueue.Enqueue(domainEvent);
        }

        _httpContextAccessor.HttpContext.Items[QUEUE_NAME] = domainEventsQueue;
    }

    /// <summary>
    /// Gets the events.
    /// </summary>
    /// <returns></returns>
    public Queue<IDomainEvent> GetEvents()
    {
        if (_httpContextAccessor?.HttpContext is null) return _empty;

        if (_httpContextAccessor.HttpContext.Items.TryGetValue(QUEUE_NAME, out var value) &&
        value is Queue<IDomainEvent> domainEventsQueue) return domainEventsQueue;

        return _empty;
    }
}
