using Kensho.CleanArchitecture.Domain.Events;
using MediatR;

namespace Kensho.CleanArchitecture.Infrastructure.MediatR.Events;

/// <summary>
/// MediatR notification that wraps a domain event without coupling the domain model to MediatR.
/// </summary>
/// <typeparam name="TDomainEvent">The type of the domain event.</typeparam>
public sealed record DomainEventNotification<TDomainEvent>(TDomainEvent DomainEvent) : INotification
    where TDomainEvent : IDomainEvent;
