using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Kensho.CleanArchitecture.Application.Common.Persistence;

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
        services.AddDbContext<TDbContext>(optionsAction)
            .AddScoped<IUnitOfWork>(provider => provider.GetRequiredService<TDbContext>())
            .AddSingleton<ISpecificationFactory, SpecificationFactory>();

        return services;
    }
}
