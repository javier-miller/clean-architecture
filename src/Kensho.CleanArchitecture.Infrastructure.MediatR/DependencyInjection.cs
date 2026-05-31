using Kensho.CleanArchitecture.Domain.Events;
using Kensho.CleanArchitecture.Infrastructure.MediatR.Events;
using Kensho.CleanArchitecture.Infrastructure.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Kensho.CleanArchitecture.Infrastructure.MediatR;

/// <summary>
/// Dependency Injection
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Adds MediatR-based domain event dispatching.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <returns></returns>
    public static IServiceCollection AddMediatRDomainEvents(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.TryAddScoped<IDomainEventsManager, HttpContextDomainEventsManager>();
        services.TryAddScoped<IDomainEventsDispatcher, MediatRDomainEventsDispatcher>();

        return services;
    }

    /// <summary>
    /// Adds MediatR and MediatR-based domain event dispatching.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <param name="configuration">The MediatR configuration.</param>
    /// <returns></returns>
    public static IServiceCollection AddMediatRDomainEvents(
        this IServiceCollection services,
        Action<MediatRServiceConfiguration> configuration)
    {
        services.AddMediatR(configuration);

        return services.AddMediatRDomainEvents();
    }

    /// <summary>
    /// Uses MediatR-based domain event dispatching.
    /// </summary>
    /// <param name="app">The application builder.</param>
    /// <returns></returns>
    public static IApplicationBuilder UseMediatRDomainEvents(this IApplicationBuilder app) =>
        app.UseMiddleware<MediatRDomainEventsMiddleware>();
}
