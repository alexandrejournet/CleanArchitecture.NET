using CleanArchitecture.Domain.Base;
using CleanArchitecture.Domain.DTO.Request;
using CleanArchitecture.Infrastructure.Base;
using CleanArchitecture.Infrastructure.Specifications;
using System.Linq.Expressions;
using System.Security.Claims;

namespace CleanArchitecture.Application.Base
{
    public class BaseService<TRepository, T> : IBaseService<T>
        where TRepository : IBaseRepository<T>
        where T : BaseEntity
    {

        protected readonly TRepository _repository;

        public BaseService(TRepository repository)
        {
            _repository = repository;
        }

        public ClaimsPrincipal GetCurrentAuth()
        {
            return _repository.GetCurrentAuth();
        }
        public async Task<T> GetById(int id)
        {
            return await _repository.GetById(id);
        }

        public async Task<T> GetBy(Expression<Func<T, bool>> where)
        {
            return await _repository.GetBy(where);
        }
        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> where)
        {
            return await _repository.GetAllAsync(where);
        }
        public async Task<List<T>> GetAllAsync(ISpecification<T> baseSpecifications = null)
        {
            return await _repository.GetAllAsync(baseSpecifications);
        }

        public async Task<List<T>> GetAllAsync(string query, ISpecification<T> baseSpecifications = null)
        {
            return await _repository.GetAllAsync(query, baseSpecifications);
        }

        /*public async Task<List<T>> GetAllAsync(string query, List<MySqlParameter> parameters, IBaseSpecifications<T> baseSpecifications = null)
        {
            return await _repository.GetAllAsync(query, parameters, baseSpecifications);
        }*/

        public async Task<decimal> CountAllAsync()
        {
            return await _repository.CountAllAsync();
        }

        public async Task<decimal> CountAllAsync(Expression<Func<T, bool>> where)
        {
            return await _repository.CountAllAsync(where);
        }

        public async Task<List<T>> PageAllAsync(PageRequest? pageRequest, ISpecification<T>? baseSpecifications)
        {
            return await _repository.PageAllAsync(pageRequest, baseSpecifications);
        }
    }
}
