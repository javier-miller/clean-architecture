using Microsoft.Extensions.DependencyInjection;
using Kensho.CleanArchitecture.Application.Common.Persistence;

namespace Kensho.CleanArchitecture.Infrastructure.Persistence;

/// <summary>
/// Repository Factory
/// </summary>
/// <seealso cref="IRepositoryFactory" />
public class RepositoryFactory : IRepositoryFactory
{
    private IServiceProvider _serviceProvider;

    /// <summary>
    /// Initializes a new instance of the <see cref="RepositoryFactory"/> class.
    /// </summary>
    /// <param name="serviceProvider">The service provider.</param>
    /// <exception cref="ArgumentNullException"></exception>
    public RepositoryFactory(IServiceProvider serviceProvider)
    {
        ArgumentNullException.ThrowIfNull(serviceProvider);

        _serviceProvider = serviceProvider;
    }

    /// <summary>
    /// Gets the read repository.
    /// </summary>
    /// <typeparam name="TRepository">The type of the repository.</typeparam>
    /// <returns></returns>
    public TRepository GetReadRepository<TRepository>()
        where TRepository : IRepository
    {
        return _serviceProvider.GetRequiredKeyedService<TRepository>("readonly");
    }

    /// <summary>
    /// Gets the repository.
    /// </summary>
    /// <typeparam name="TRepository">The type of the repository.</typeparam>
    /// <returns></returns>
    public TRepository GetRepository<TRepository>()
        where TRepository : IRepository
    {
        return _serviceProvider.GetRequiredService<TRepository>();
    }
}

