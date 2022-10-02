using CleanArchitecture.Domain.Base;
using CleanArchitecture.Domain.Request;
using CleanArchitecture.Infrastructure.Specifications.Base;
using System.Linq.Expressions;
using System.Security.Claims;

namespace CleanArchitecture.Infrastructure.Base
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<T> GetById(int id);
        Task<T> GetBy(Expression<Func<T, bool>> where);
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> where);
        Task<List<T>> GetAllAsync(IBaseSpecifications<T>? baseSpecifications = null);
        Task<List<T>> GetAllAsync(string query, IBaseSpecifications<T>? baseSpecifications = null);
        // Task<List<T>> GetAllAsync(string query, List<MySqlParameter> parameters, IBaseSpecifications<T>? baseSpecifications = null);



        IQueryable<T> GetAllQueryable(Expression<Func<T, bool>> where);
        IQueryable<T> GetAllQueryable(IBaseSpecifications<T>? baseSpecifications = null);
        IQueryable<T> GetAllQueryable(string query, IBaseSpecifications<T>? baseSpecifications = null);
        //IQueryable<T> GetAllQueryable(string query, List<MySqlParameter> parameters, IBaseSpecifications<T>? baseSpecifications = null);


        Task<List<T>> PageAllAsync(PageRequest? pageRequest, IBaseSpecifications<T>? baseSpecifications);
        IQueryable<T> PageAllQueryable(PageRequest? pageRequest, IBaseSpecifications<T>? baseSpecifications);

        Task<decimal> CountAllAsync();
        Task<decimal> CountAllAsync(Expression<Func<T, bool>> where);

        Task<bool> AnyAsync(Expression<Func<T, bool>> where);
        ClaimsPrincipal GetCurrentAuth();
    }
}
