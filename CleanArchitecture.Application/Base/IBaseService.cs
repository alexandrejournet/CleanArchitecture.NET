using CleanArchitecture.Domain.Base;
using CleanArchitecture.Domain.DTO.Request;
using CleanArchitecture.Infrastructure.Specifications;
using System.Linq.Expressions;
using System.Security.Claims;

namespace CleanArchitecture.Application.Base
{
    public interface IBaseService<T> where T : BaseEntity
    {
        Task<T> GetById(int id);
        Task<T> GetBy(Expression<Func<T, bool>> where);
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> where);
        Task<List<T>> GetAllAsync(ISpecification<T>? baseSpecifications = null);
        Task<List<T>> GetAllAsync(string query, ISpecification<T>? baseSpecifications = null);
        //Task<List<T>> GetAllAsync(string query, List<MySqlParameter> parameters, IBaseSpecifications<T>? baseSpecifications = null);


        Task<List<T>> PageAllAsync(PageRequest? pageRequest = null, ISpecification<T>? baseSpecifications = null);

        Task<decimal> CountAllAsync();
        Task<decimal> CountAllAsync(Expression<Func<T, bool>> where);
        ClaimsPrincipal GetCurrentAuth();
    }
}
