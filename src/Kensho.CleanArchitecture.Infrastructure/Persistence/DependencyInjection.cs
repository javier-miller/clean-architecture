using Kensho.CleanArchitecture.Application.Common.Persistence;
using Kensho.CleanArchitecture.Domain.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Kensho.CleanArchitecture.Infrastructure.Persistence;

/// <summary>
/// Dependency Injection
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// Adds the infrastructure core.
    /// </summary>
    /// <typeparam name="TDbContext">The type of the database context.</typeparam>
    /// <param name="services">The services.</param>
    /// <param name="optionsAction">The options action.</param>
    /// <returns></returns>
    public static IServiceCollection AddInfrastructureCore<TDbContext>(this IServiceCollection services, Action<DbContextOptionsBuilder> optionsAction)
            where TDbContext : DbContext, IUnitOfWork
    {
        ArgumentNullException.ThrowIfNull(optionsAction);

        services.AddHttpContextAccessor();
        services.TryAddScoped<IDomainEventsManager, HttpContextDomainEventsManager>();
        services.TryAddScoped<IRepositoryFactory, RepositoryFactory>();
        services.TryAddSingleton<ISpecificationFactory, SpecificationFactory>();

        services.AddDbContext<TDbContext>(optionsAction)
            .AddScoped<IUnitOfWork>(provider => provider.GetRequiredService<TDbContext>());

        services.AddKeyedScoped("readonly", (provider, _) =>
        {
            var optionsBuilder = new DbContextOptionsBuilder<TDbContext>();
            optionsAction(optionsBuilder);
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

            return ActivatorUtilities.CreateInstance<TDbContext>(provider, optionsBuilder.Options);
        });

        return services;
    }
}
