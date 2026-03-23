using Kensho.CleanArchitecture.Application.Common.Persistence;
using Kensho.CleanArchitecture.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace Kensho.CleanArchitecture.Infrastructure.Persistence;

public static class DbContextExtensions
{
    public static IServiceCollection AddSpecificationFactory(this IServiceCollection services) => 
        services.AddSingleton<ISpecificationFactory, SpecificationFactory>();

    public static IServiceCollection AddRepository<TService, TImplementation, TEntity, TDbContext>(this IServiceCollection services)
        where TService : class, IRepository<TEntity>
        where TImplementation : class, TService
        where TEntity : Entity
        where TDbContext : ApplicationDbContext<TDbContext>
    {
        services.AddKeyedScoped<TService, TImplementation>("readonly", (provider, key) =>
        {
            var dbContext = provider.GetRequiredKeyedService<TDbContext>(key);
            var result = ActivatorUtilities.CreateInstance<TImplementation>(provider, dbContext);

            return result;
        });

        services.AddScoped<TService, TImplementation>();

        return services;
    }
}
