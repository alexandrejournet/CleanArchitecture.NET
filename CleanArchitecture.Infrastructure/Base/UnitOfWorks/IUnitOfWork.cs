using CleanArchitecture.Infrastructure.Base.Repository;

namespace CleanArchitecture.Infrastructure.Base.UnitOfWorks
{
    public interface IUnitOfWork : IDisposable
    {
        TRepository GetRepository<TRepository, TEntity>() where TRepository : IBaseRepository<TEntity> where TEntity : class;
    }
}
