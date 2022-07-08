using CleanArchitecture.Infrastructure.Interfaces;
using CleanArchitecture.Models.Base;
using System.Linq.Expressions;

namespace CleanArchitecture.Applications.Base
{
    public class BaseService<TRepository, TEntity> : IBaseService<TEntity>
        where TRepository : IBaseRepository<TEntity>
        where TEntity : BaseEntity
    {

        protected readonly TRepository _repository;

        public BaseService(TRepository repository)
        {
            _repository = repository;
        }
        public async Task<TEntity> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> where)
        {
            return await _repository.GetAllAsync(where);
        }
    }
}
