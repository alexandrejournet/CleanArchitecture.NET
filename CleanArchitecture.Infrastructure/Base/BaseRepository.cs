using CleanArchitecture.Domain.Base;
using CleanArchitecture.Domain.DTO.Request;
using CleanArchitecture.Infrastructure.Database;
using CleanArchitecture.Infrastructure.Specifications;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Security.Claims;

namespace CleanArchitecture.Infrastructure.Base
{
    public class BaseRepository<T, TContext> : IBaseRepository<T>
        where T : BaseEntity
        where TContext : CoreDbContext
    {
        protected readonly TContext _dbContext;
        private readonly IHttpContextAccessor _httpContext;
        protected readonly CancellationToken _cancellationToken;

        public BaseRepository(TContext context, IHttpContextAccessor httpContext)
        {
            _dbContext = context;
            _httpContext = httpContext;
            _cancellationToken = httpContext.HttpContext.RequestAborted;
        }

        public ClaimsPrincipal GetCurrentAuth()
        {
            return _httpContext.HttpContext.User;
        }

        public async Task<T> GetById(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<T> GetBy(Expression<Func<T, bool>> where)
        {
            return await SpecificationEvaluator<T>
                        .GetQuery(_dbContext.Set<T>().AsQueryable(), new Specification<T>(where))
                        .AsNoTracking()
                        .FirstOrDefaultAsync(_cancellationToken);
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> where)
        {
            return await GetAllQueryable(where).ToList(_cancellationToken);
        }

        public async Task<List<T>> GetAllAsync(ISpecification<T>? baseSpecifications)
        {
            return await GetAllQueryable(baseSpecifications).ToList(_cancellationToken);
        }

        public async Task<List<T>> GetAllAsync(string query, ISpecification<T>? baseSpecifications)
        {
            try
            {
                return await GetAllQueryable(query, baseSpecifications).ToList(_cancellationToken);
            }
            catch (Exception ex)
            {
                throw new Exception($"Impossible de récupérer les entités: {ex.Message}");
            }
        }

        /*public async Task<List<T>> GetAllAsync(string query, List<MySqlParameter> parameters, IBaseSpecifications<T>? baseSpecifications)
        {
            return await GetAllQueryable(query, parameters, baseSpecifications).ToList(_cancellationToken);
        }*/

        public async Task<decimal> CountAllAsync()
        {
            return await SpecificationEvaluator<T>.GetQuery(_dbContext.Set<T>()
                   .AsQueryable(), null)
                   .AsNoTracking()
                   .CountAsync(_cancellationToken);
        }

        public async Task<decimal> CountAllAsync(Expression<Func<T, bool>> where)
        {
            return await SpecificationEvaluator<T>.GetQuery(_dbContext.Set<T>()
                   .AsQueryable(), new Specification<T>(where))
                   .AsNoTracking()
                   .CountAsync(_cancellationToken);
        }

        public void SaveAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> GetAllQueryable(Expression<Func<T, bool>> where)
        {
            return SpecificationEvaluator<T>.GetQuery(_dbContext.Set<T>()
                    .AsQueryable(), new Specification<T>(where))
                    .AsNoTracking();
        }

        public IQueryable<T> GetAllQueryable(ISpecification<T>? baseSpecifications)
        {
            return SpecificationEvaluator<T>.GetQuery(_dbContext.Set<T>()
                .AsQueryable(), baseSpecifications)
                .AsNoTracking();
        }

        public IQueryable<T> GetAllQueryable(string query, ISpecification<T>? baseSpecifications)
        {
            return SpecificationEvaluator<T>.GetQuery(_dbContext.Set<T>()
                .FromSqlRaw(query), baseSpecifications)
                .AsNoTracking();
        }

        /*public IQueryable<T> GetAllQueryable(string query, List<MySqlParameter> parameters, IBaseSpecifications<T>? baseSpecifications)
        {
            return SpecificationEvaluator<T>.GetQuery(_dbContext.Set<T>()
                .FromSqlRaw(query, parameters.ToArray()), baseSpecifications)
                .AsNoTracking();
        }*/

        public async Task<List<T>> PageAllAsync(PageRequest? pageRequest, ISpecification<T>? baseSpecifications)
        {
            return await PageAllQueryable(pageRequest, baseSpecifications).ToList(_cancellationToken);
        }

        public IQueryable<T> PageAllQueryable(PageRequest? pageRequest, ISpecification<T>? baseSpecifications)
        {
            var pageSize = 10;
            var lineToSkip = 0;

            if (pageRequest != null)
            {
                pageSize = pageRequest.PageSize;
                lineToSkip = pageRequest.PageNumber > 1 ? (pageRequest.PageNumber - 1) * pageSize : 0;
            }

            return SpecificationEvaluator<T>.GetQuery(_dbContext.Set<T>()
                .AsQueryable(), baseSpecifications)
                .Skip(lineToSkip)
                .Take(pageSize)
                .AsNoTracking();
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> where)
        {
            return await SpecificationEvaluator<T>.GetQuery(_dbContext.Set<T>()
                   .AsQueryable(), new Specification<T>(where))
                   .AsNoTracking()
                   .AnyAsync(_cancellationToken);
        }
    }

    public static class RepositoryExtension
    {
        public static async Task<List<T>> ToList<T>(this IQueryable<T> list, CancellationToken cancellationToken)
        {
            return await list.ToListAsync(cancellationToken);
        }
    }
}
