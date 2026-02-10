using MediatR;

namespace Kensho.CleanArchitecture.Domain.Events;

/// <summary>
/// Domain Event Interface
/// </summary>
/// <seealso cref="INotification" />
public interface IDomainEvent : INotification;
