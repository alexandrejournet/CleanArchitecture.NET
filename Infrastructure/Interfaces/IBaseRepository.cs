using CleanArchitecture.Models.Base;
using System.Linq.Expressions;

namespace CleanArchitecture.Infrastructure.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity> GetById(int id);
        Task<List<TEntity>> GetAllAsync();
        Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> where);
    }
}
