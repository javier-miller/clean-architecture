namespace Kensho.CleanArchitecture.Infrastructure.MediatR.Events;

/// <summary>
/// Dispatches pending domain events through the configured integration mechanism.
/// </summary>
public interface IDomainEventsDispatcher
{
    /// <summary>
    /// Dispatches all pending domain events.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task DispatchAsync(CancellationToken cancellationToken = default);
}
