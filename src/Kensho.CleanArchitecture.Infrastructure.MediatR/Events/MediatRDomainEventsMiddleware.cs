using Microsoft.AspNetCore.Http;

namespace Kensho.CleanArchitecture.Infrastructure.MediatR.Events;

/// <summary>
/// Dispatches queued domain events after the request pipeline has completed successfully.
/// </summary>
public class MediatRDomainEventsMiddleware
{
    private readonly RequestDelegate _next;

    /// <summary>
    /// Initializes a new instance of the <see cref="MediatRDomainEventsMiddleware"/> class.
    /// </summary>
    /// <param name="next">The next request delegate.</param>
    /// <exception cref="ArgumentNullException"></exception>
    public MediatRDomainEventsMiddleware(RequestDelegate next)
    {
        ArgumentNullException.ThrowIfNull(next);

        _next = next;
    }

    /// <summary>
    /// Invokes the middleware.
    /// </summary>
    /// <param name="context">The HTTP context.</param>
    /// <param name="dispatcher">The domain events dispatcher.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public async Task InvokeAsync(HttpContext context, IDomainEventsDispatcher dispatcher)
    {
        await _next(context);

        await dispatcher.DispatchAsync(context.RequestAborted);
    }
}
