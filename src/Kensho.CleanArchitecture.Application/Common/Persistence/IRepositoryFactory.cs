namespace Kensho.CleanArchitecture.Application.Common.Persistence;

public interface IRepositoryFactory
{
    TRepository GetReadRepository<TRepository>()
        where TRepository : IRepository;

    TRepository GetRepository<TRepository>()
        where TRepository : IRepository;
}
