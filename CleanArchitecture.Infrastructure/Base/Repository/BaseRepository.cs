using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Domain.Utils;
using CleanArchitecture.Infrastructure.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data.Common;
using System.Linq.Expressions;
using System.Security.Claims;

namespace CleanArchitecture.Infrastructure.Base.Repository
{
    public class BaseRepository<T, TContext> : IBaseRepository<T>
    where T : class
    where TContext : DbContext
    {
        protected readonly CancellationToken _cancellationToken;
        protected readonly TContext _dbContext;
        protected readonly IHttpContextAccessor _httpContext;
        protected readonly IMapper _mapper;

        /// <summary>
        /// Constructs a new instance of CoreRepository with the given context and Http context.
        /// </summary>
        /// <param name="dbContext">The DbContext to use.</param>
        /// <param name="httpContext">The HttpContext to use for cancellations.</param>
        protected BaseRepository(TContext dbContext, IHttpContextAccessor httpContext)
        {
            _dbContext = dbContext;
            _cancellationToken = httpContext.HttpContext?.RequestAborted ?? CancellationToken.None;
            _httpContext = httpContext;
        }

        public BaseRepository(TContext context, IHttpContextAccessor httpContext, IMapper mapper)
        {
            _cancellationToken = httpContext.HttpContext!.RequestAborted;
            _dbContext = context;
            _httpContext = httpContext;
            _mapper = mapper;
        }

        public ClaimsPrincipal GetCurrentAuth()
        {
            return _httpContext.HttpContext.User;
        }

        /// <summary>
        /// Adds the given entity to the database and saves the changes.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        public async Task AddAsync(T entity)
        {
            await _dbContext.AddAsync(entity, _cancellationToken);
        }

        public async Task AddAsync<TEntity>(TEntity entity) where TEntity : class
        {
            await _dbContext.Set<TEntity>().AddAsync(entity, _cancellationToken);
        }

        public async Task AddAndSaveAsync(T entity)
        {
            await AddAsync(entity);
            await SaveChangesAsync();
        }

        public async Task AddAndSaveAsync<TEntity>(TEntity entity) where TEntity : class
        {
            await AddAsync(entity);
            await SaveChangesAsync();
        }

        /// <summary>
        /// Adds a range of entities to the database and saves the changes.
        /// </summary>
        /// <param name="entities">The entities to add.</param>
        public async Task AddRangeAsync(ICollection<T> entities)
        {
            await _dbContext.AddRangeAsync(entities, _cancellationToken);
        }

        public async Task AddRangeAsync<TEntity>(ICollection<TEntity> entities) where TEntity : class
        {
            await _dbContext.Set<TEntity>().AddRangeAsync(entities, _cancellationToken);
        }

        public async Task AddRangeAndSaveAsync(ICollection<T> entities)
        {
            await AddRangeAsync(entities);
            await SaveChangesAsync();
        }

        public async Task AddRangeAndSaveAsync<TEntity>(ICollection<TEntity> entities) where TEntity : class
        {
            await AddRangeAsync(entities);
            await SaveChangesAsync();
        }

        /// <summary>
        /// Checks if any entities in the database match the given expression.
        /// </summary>
        /// <param name="where">The expression to evaluate.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean
        /// indicating whether any entities match the expression.</returns>
        public async Task<bool> AnyAsync(Expression<Func<T, bool>> where)
        {
            return await _dbContext.Set<T>().Where(where).AnyAsync(_cancellationToken);
        }

        public async Task<bool> AnyAsync<TEntity>(Expression<Func<TEntity, bool>> where) where TEntity : class
        {
            return await ApplySpecificationWhere(where).AnyAsync(_cancellationToken);
        }
        public async Task<bool> AnyAsync<TEntity>(IQueryable<TEntity> queryable) where TEntity : class
        {
            return await queryable.AnyAsync(_cancellationToken);
        }
        /// <summary>
        /// Begins a new transaction in the database.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the IDbContextTransaction
        /// that encapsulates all changes made to the DbContext within the transaction.</returns>
        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _dbContext.Database.BeginTransactionAsync(_cancellationToken);
        }

        /// <summary>
        /// Counts all entities in the database.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the count of all entities.</returns>
        public async Task<decimal> CountAsync()
        {
            return await CountAsync();
        }

        /// <summary>
        /// Counts all entities in the database.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the count of all entities.</returns>
        public async Task<decimal> CountAsync<TEntity>() where TEntity : class
        {
            return await CountAsync<TEntity>();
        }

        /// <summary>
        /// Counts all entities in the database that satisfy the given expression.
        /// </summary>
        /// <param name="where">The expression to evaluate.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the count of entities that satisfy the expression.</returns>
        public async Task<decimal> CountAsync(Expression<Func<T, bool>>? where = null)
        {
            if (where == null)
            {
                return await _dbContext.Set<T>().CountAsync(_cancellationToken);
            }
            return await _dbContext.Set<T>().Where(where).CountAsync(_cancellationToken);
        }

        public async Task<decimal> CountAsync<TEntity>(Expression<Func<TEntity, bool>>? where = null) where TEntity : class
        {
            if (where == null)
            {
                return await _dbContext.Set<TEntity>().CountAsync(_cancellationToken);
            }
            return await _dbContext.Set<TEntity>().Where(where).CountAsync(_cancellationToken);
        }

        public async Task<T> FindAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<TEntity> FindAsync<TEntity>(int id) where TEntity : class
        {
            return await _dbContext.Set<TEntity>().FindAsync(id);
        }

        /// <summary>
        /// Gets all entities from the database.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the list of all entities.</returns>
        public async Task<ICollection<T>> GetAllAsync()
        {
            return await GetAllQueryable().ToCollectionAsync(_cancellationToken);
        }
        /// <summary>
        /// Gets all entities from the database that satisfy the given expression.
        /// </summary>
        /// <param name="where">The expression to evaluate.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the list of entities that satisfy the expression.</returns>
        public async Task<ICollection<T>> GetAllAsync(Expression<Func<T, bool>> where)
        {
            return await GetAllQueryable(where).ToCollectionAsync(_cancellationToken);
        }
        /// <summary>
        /// Gets all entities from the database based on the provided query and specifications.
        /// </summary>
        /// <param name="query">The SQL query to execute.</param>
        /// <param name="coreSpecifications">The core specifications to evaluate.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the list of entities fetched based on the query and specifications.</returns>
        public async Task<ICollection<T>> GetAllAsync(string query)
        {
            return await GetAllQueryable(query).ToCollectionAsync(_cancellationToken);
        }
        /// <summary>
        /// Gets all entities from the database based on the provided query, parameters and specifications.
        /// </summary>
        /// <param name="query">The SQL query to execute.</param>
        /// <param name="parameters">The SQL parameters needed for the query.</param>
        /// <param name="coreSpecifications">The core specifications to evaluate.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the list of entities fetched based on the query, parameters, and specifications.</returns>
        public async Task<ICollection<T>> GetAllAsync(string query, ICollection<DbParameter> parameters)
        {
            return await GetAllQueryable(query, parameters).ToCollectionAsync(_cancellationToken);
        }
        public async Task<ICollection<TEntity>> GetAllAsync<TEntity>() where TEntity : class
        {
            return await ApplySpecification<TEntity>().ToCollectionAsync(_cancellationToken);
        }
        public async Task<ICollection<TEntity>> GetAllAsync<TEntity>(Expression<Func<TEntity, bool>> where) where TEntity : class
        {
            return await ApplySpecificationWhere(where).ToCollectionAsync(_cancellationToken);
        }
        public async Task<ICollection<TEntity>> GetAllAsync<TEntity>(string query) where TEntity : class
        {
            return await ApplySpecificationQuery<TEntity>(query).ToCollectionAsync(_cancellationToken);
        }
        public async Task<ICollection<TEntity>> GetAllAsync<TEntity>(string query, ICollection<DbParameter> parameters) where TEntity : class
        {
            return await ApplySpecificationQuery<TEntity>(query, parameters).ToCollectionAsync(_cancellationToken);
        }
        /// <summary>
        /// Gets all entities from the database by executing the provided IQueryable query.
        /// </summary>
        /// <param name="query">The IQueryable query to execute.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the list of entities fetched by executing the query.</returns>
        public async Task<ICollection<TEntity>> GetAllByQueryable<TEntity>(IQueryable<TEntity> query)
        {
            return await query.ToCollectionAsync(_cancellationToken);
        }
        /// <summary>
        /// Gets all entities from the database satisfying the specifications provided.
        /// </summary>
        /// <param name="coreSpecifications">The core specifications to evaluate.</param>
        /// <returns>IQueryable of entities satisfying the specifications.</returns>
        public IQueryable<T> GetAllQueryable()
        {
            return ApplySpecification().AsNoTracking();
        }

        /// <summary>
        /// Gets entities from the database based on the SQL query and specifications provided.
        /// </summary>
        /// <param name="query">The SQL query to execute.</param>
        /// <param name="coreSpecifications">The core specifications to evaluate.</param>
        /// <returns>IQueryable of entities fetched based on the query and specifications.</returns>
        public IQueryable<T> GetAllQueryable(string query)
        {
            return ApplySpecificationQuery(query)
                .AsNoTracking();
        }

        /// <summary>
        /// Gets entities from the database based on the SQL query, parameters, and specifications provided.
        /// </summary>
        /// <param name="query">The SQL query to execute.</param>
        /// <param name="parameters">The SQL parameters needed for the query.</param>
        /// <param name="coreSpecifications">The core specifications to evaluate.</param>
        /// <returns>IQueryable of entities fetched based on the query, parameters, and specifications.</returns>
        public IQueryable<T> GetAllQueryable(string query, ICollection<DbParameter> parameters)
        {
            return ApplySpecificationQuery(query, parameters)
                .AsNoTracking();
        }
        /// <summary>
        /// Gets all entities from the database that matches the given expression as IQueryable.
        /// </summary>
        /// <param name="where">The expression to evaluate.</param>
        /// <returns>IQueryable of entities that match the expression.</returns>
        public IQueryable<T> GetAllQueryable(Expression<Func<T, bool>> where)
        {
            return ApplySpecificationWhere(where)
                .AsNoTracking();
        }

        /// <summary>
        /// Gets all entities from the database satisfying the specifications provided.
        /// </summary>
        /// <param name="coreSpecifications">The core specifications to evaluate.</param>
        /// <returns>IQueryable of entities satisfying the specifications.</returns>
        public IQueryable<TEntity> GetAllQueryable<TEntity>() where TEntity : class
        {
            return ApplySpecification<TEntity>().AsNoTracking();
        }

        /// <summary>
        /// Gets entities from the database based on the SQL query.
        /// </summary>
        /// <param name="query">The SQL query to execute.</param>
        /// <param name="coreSpecifications">The core specifications to evaluate.</param>
        /// <returns>IQueryable of entities fetched based on the query and specifications.</returns>
        public IQueryable<TEntity> GetAllQueryable<TEntity>(string query) where TEntity : class
        {
            return ApplySpecificationQuery<TEntity>(query).AsNoTracking();
        }

        /// <summary>
        /// Gets entities from the database based on the SQL query, parameters, and specifications provided.
        /// </summary>
        /// <param name="query">The SQL query to execute.</param>
        /// <param name="parameters">The SQL parameters needed for the query.</param>
        /// <param name="coreSpecifications">The core specifications to evaluate.</param>
        /// <returns>IQueryable of entities fetched based on the query, parameters, and specifications.</returns>
        public IQueryable<TEntity> GetAllQueryable<TEntity>(string query, ICollection<DbParameter> parameters) where TEntity : class
        {
            return ApplySpecificationQuery<TEntity>(query, parameters)
                .AsNoTracking();
        }
        /// <summary>
        /// Gets all entities from the database that matches the given expression as IQueryable.
        /// </summary>
        /// <param name="where">The expression to evaluate.</param>
        /// <returns>IQueryable of entities that match the expression.</returns>
        public IQueryable<TEntity> GetAllQueryable<TEntity>(Expression<Func<TEntity, bool>> where) where TEntity : class
        {
            return ApplySpecificationWhere(where)
                .AsNoTracking();
        }

        /// <summary>
        /// Gets the first entity that satisfies the provided expression.
        /// </summary>
        /// <param name="where">The expression to evaluate.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the first entity that satisfies the expression or null if no such entity exists.</returns>
        public async Task<T?> GetBy(Expression<Func<T, bool>> where)
        {
            return await ApplySpecificationWhere(where)
                .AsNoTracking()
                .FirstOrDefaultAsync(_cancellationToken);
        }
        /// <summary>
        /// Gets the first entity that satisfies the provided specifications.
        /// </summary>
        /// <param name="coreSpecifications">The specifications to evaluate.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the first entity that satisfies the specifications or null if no such entity exists.</returns>
        public async Task<T?> GetBy()
        {
            return await ApplySpecification()
                .AsNoTracking()
                .FirstOrDefaultAsync(_cancellationToken);
        }
        public async Task<T?> GetBy(string query, ICollection<DbParameter> parameters)
        {
            return await ApplySpecificationQuery(query, parameters).FirstOrDefaultAsync(_cancellationToken);
        }
        public async Task<TEntity?> GetBy<TEntity>(Expression<Func<TEntity, bool>> where) where TEntity : class
        {
            return await ApplySpecificationWhere(where).FirstOrDefaultAsync(_cancellationToken);
        }
        public async Task<TEntity?> GetBy<TEntity>() where TEntity : class
        {
            return await ApplySpecification<TEntity>().FirstOrDefaultAsync(_cancellationToken);
        }
        public async Task<TEntity?> GetBy<TEntity>(string query, ICollection<DbParameter> parameters) where TEntity : class
        {
            return await ApplySpecificationQuery<TEntity>(query, parameters).FirstOrDefaultAsync(_cancellationToken);
        }
        public async Task<Paged<T>> GetPagedResult(Page page)
        {
            return new()
            {
                Items = await ApplySpecification()
                    .Pageable(page)
                    .AsNoTracking()
                    .ToCollectionAsync(_cancellationToken),
                Count = await ApplySpecification().CountAsync(_cancellationToken)
            };
        }
        public async Task<Paged<T>> GetPagedResult(Page page, string query, ICollection<DbParameter> parameters)
        {
            return new()
            {
                Items = await ApplySpecificationQuery(query, parameters)
                    .Pageable(page)
                    .AsNoTracking()
                    .ToCollectionAsync(_cancellationToken),
                Count = await ApplySpecificationQuery(query, parameters)
                    .CountAsync(_cancellationToken)
            };
        }
        public async Task<Paged<TEntity>> GetPagedResult<TEntity>(Page page) where TEntity : class
        {
            return new()
            {
                Items = await ApplySpecification<TEntity>()
                    .Pageable(page)
                    .AsNoTracking()
                    .ToCollectionAsync(_cancellationToken),
                Count = await ApplySpecification<TEntity>()
                    .CountAsync(_cancellationToken)
            };
        }
        public async Task<Paged<TEntity>> GetPagedResult<TEntity>(Page page, string query, ICollection<DbParameter> parameters) where TEntity : class
        {
            return new()
            {
                Items = await ApplySpecificationQuery<TEntity>(query, parameters)
                    .Pageable(page)
                    .AsNoTracking()
                    .ToCollectionAsync(_cancellationToken),
                Count = await ApplySpecificationQuery<TEntity>(query, parameters)
                    .CountAsync(_cancellationToken)
            };
        }
        /// <summary>
        /// Pages all entities based on the provided page information and specifications.
        /// </summary>
        /// <param name="page">The page information.</param>
        /// <param name="coreSpecifications">The specifications to evaluate.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of entities paged according to the provided information and specifications.</returns>
        public async Task<ICollection<T>> PageAllAsync(Page page)
        {
            return await PageAllQueryable(page).ToCollectionAsync(_cancellationToken);
        }
        /// <summary>
        /// Returns an IQueryable of all paged entities based on the provided page information and specifications.
        /// </summary>
        /// <param name="page">The page information.</param>
        /// <param name="coreSpecifications">The specifications to evaluate.</param>
        /// <returns>IQueryable of entities paged according to the provided page information and specifications.</returns>
        public IQueryable<T> PageAllQueryable(Page page)
        {
            page ??= new Page();

            return ApplySpecification()
                .Pageable(page)
                .AsNoTracking();
        }
        /// <summary>
        /// Removes the given entity from the database and saves the changes.
        /// </summary>
        /// <param name="entity">The entity to remove.</param>
        public void Remove(T entity)
        {
            _dbContext.Remove(entity);
        }

        public void Remove<TEntity>(TEntity entity) where TEntity : class
        {
            _dbContext.Set<TEntity>().Remove(entity);
        }
        public async Task RemoveAndSaveAsync(T entity)
        {
            Remove(entity);
            await SaveChangesAsync();
        }
        public async Task RemoveAndSaveAsync<TEntity>(TEntity entity) where TEntity : class
        {
            Remove(entity);
            await SaveChangesAsync();
        }
        /// <summary>
        /// Removes a range of entities from the database and saves the changes.
        /// </summary>
        /// <param name="entities">The entities to removal.</param>
        public void RemoveRange(ICollection<T> entities)
        {
            _dbContext.RemoveRange(entities);
        }
        public void RemoveRange<TEntity>(ICollection<TEntity> entities) where TEntity : class
        {
            _dbContext.Set<TEntity>().RemoveRange(entities);
        }
        public async Task RemoveRangeAndSaveAsync(ICollection<T> entities)
        {
            RemoveRange(entities);
            await SaveChangesAsync();
        }
        public async Task RemoveRangeAndSaveAsync<TEntity>(ICollection<TEntity> entities) where TEntity : class
        {
            RemoveRange(entities);
            await SaveChangesAsync();
        }
        /// <summary>
        /// Saves changes in the DbContext to the database.
        /// </summary>
        /// <returns>A task represents the asynchronous operation for saving changes to the database.</returns>
        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync(_cancellationToken);
        }

        /// <summary>
        /// Updates the provided entity in the DbContext and saves the changes to the database.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <returns>A task representing the asynchronous operation of updating the entity and saving changes to the database.</returns>
        public void Update(T entity)
        {
            _dbContext.Update(entity);
        }

        public void Update<TEntity>(TEntity entity) where TEntity : class
        {
            _dbContext.Set<TEntity>().Update(entity);
        }
        public async Task UpdateAndSaveAsync(T entity)
        {
            Update(entity);
            await SaveChangesAsync();
        }
        public async Task UpdateAndSaveAsync<TEntity>(TEntity entity) where TEntity : class
        {
            Update(entity);
            await SaveChangesAsync();
        }
        /// <summary>
        /// Updates the range of entities in the DbContext and saves the changes to the database.
        /// </summary>
        /// <param name="entities">The entities to update.</param>
        /// <returns>A task representing async operation of updating the entities and saving changes to the database.</returns>
        public void UpdateRange(ICollection<T> entities)
        {
            _dbContext.UpdateRange(entities);
        }

        public void UpdateRange<TEntity>(ICollection<TEntity> entities) where TEntity : class
        {
            _dbContext.Set<TEntity>().UpdateRange(entities);
        }
        public async Task UpdateRangeAndSaveAsync(ICollection<T> entities)
        {
            UpdateRange(entities);
            await SaveChangesAsync();
        }
        public async Task UpdateRangeAndSaveAsync<TEntity>(ICollection<TEntity> entities) where TEntity : class
        {
            UpdateRange(entities);
            await SaveChangesAsync();
        }

        public async Task ExecuteQuery(string query, ICollection<DbParameter>? dbParameters)
        {
            if (dbParameters is not null)
            {
                await _dbContext.Database.ExecuteSqlRawAsync(query, dbParameters, _cancellationToken);
            }
            else
            {
                await _dbContext.Database.ExecuteSqlRawAsync(query, _cancellationToken);
            }

            await SaveChangesAsync();
        }

        private IQueryable<T> ApplySpecification()
        {
            return _dbContext.Set<T>().AsQueryable();
        }

        private IQueryable<TEntity> ApplySpecification<TEntity>() where TEntity : class
        {
            return _dbContext.Set<TEntity>().AsQueryable();
        }

        private IQueryable<T> ApplySpecificationQuery(string query)
        {
            return _dbContext.Set<T>()
                .FromSqlRaw(query)
                .AsQueryable();
        }

        private IQueryable<T> ApplySpecificationQuery(string query, ICollection<DbParameter> parameters)
        {
            return _dbContext.Set<T>()
                .FromSqlRaw(query, parameters.ToArray())
                .AsQueryable();
        }

        private IQueryable<TEntity> ApplySpecificationQuery<TEntity>(string query) where TEntity : class
        {
            return _dbContext.Set<TEntity>()
                .FromSqlRaw(query)
                .AsQueryable();
        }
        private IQueryable<TEntity> ApplySpecificationQuery<TEntity>(string query, ICollection<DbParameter> parameters) where TEntity : class
        {
            return _dbContext.Set<TEntity>()
                .FromSqlRaw(query, parameters.ToArray())
                .AsQueryable();
        }
        private IQueryable<T> ApplySpecificationWhere(Expression<Func<T, bool>> where)
        {
            return _dbContext.Set<T>().Where(where).AsQueryable();
        }
        private IQueryable<TEntity> ApplySpecificationWhere<TEntity>(Expression<Func<TEntity, bool>> where) where TEntity : class
        {
            return _dbContext.Set<TEntity>().Where(where).AsQueryable();
        }

        public ChangeTracker ChangeTracker()
        {
            return _dbContext.ChangeTracker;
        }


        #region Automapper
        public async Task<List<DTO>> GetAllAsyncDto<DTO>() where DTO : class
        {
            return await GetAllQueryableDto<DTO>().ToListAsync(_cancellationToken);
        }
        public async Task<List<DTO>> GetAllAsyncDto<DTO>(Expression<Func<T, bool>> where) where DTO : class
        {
            return await GetAllQueryableDto<DTO>(where).ToListAsync(_cancellationToken);
        }
        public async Task<List<DTO>> GetAllAsyncDto<DTO>(Expression<Func<T, bool>> where, Page pageRequest) where DTO : class
        {
            return await GetAllQueryableDto<DTO>(where, pageRequest).ToListAsync(_cancellationToken);
        }
        public async Task<List<DTO>> GetAllAsyncDto<DTO>(Page pageRequest) where DTO : class
        {
            return await GetAllQueryableDto<DTO>(pageRequest).ToListAsync(_cancellationToken);
        }
        public async Task<List<DTO>> GetAllAsyncDto<DTO>(string query, List<DbParameter> parameters, Page pageRequest) where DTO : class
        {
            return await GetAllQueryableDto<DTO>(query, parameters, pageRequest).ToListAsync(_cancellationToken);
        }
        public IQueryable<DTO> GetAllQueryableDto<DTO>() where DTO : class
        {
            return _dbContext.Set<T>().AsQueryable().ProjectTo<DTO>(_mapper.ConfigurationProvider).AsNoTracking();
        }
        public IQueryable<DTO> GetAllQueryableDto<DTO>(Expression<Func<T, bool>> where) where DTO : class
        {
            return _dbContext.Set<T>().AsQueryable()
                    .ProjectTo<DTO>(_mapper.ConfigurationProvider)
                    .AsNoTracking();
        }
        public IQueryable<DTO> GetAllQueryableDto<DTO>(Expression<Func<T, bool>> where, Page pageRequest) where DTO : class
        {
            IQueryable<T> query = _dbContext.Set<T>().Where(where).AsQueryable();

            if (pageRequest != null)
            {
                query = query.Pageable(pageRequest);
            }

            return query.ProjectTo<DTO>(_mapper.ConfigurationProvider).AsNoTracking();
        }
        public IQueryable<DTO> GetAllQueryableDto<DTO>(Page pageRequest) where DTO : class
        {
            IQueryable<T> query = _dbContext.Set<T>().AsQueryable();

            if (pageRequest != null)
            {
                query = query.Pageable(pageRequest);
            }

            return query.ProjectTo<DTO>(_mapper.ConfigurationProvider).AsNoTracking();
        }

        public IQueryable<DTO> GetAllQueryableDto<DTO>(string query, List<DbParameter> parameters, Page pageRequest) where DTO : class
        {
            IQueryable<T> queryable = _dbContext.Set<T>().FromSqlRaw(query, parameters);

            if (pageRequest != null)
            {
                queryable = queryable.Pageable(pageRequest);
            }

            return queryable.ProjectTo<DTO>(_mapper.ConfigurationProvider).AsNoTracking();
        }
        #endregion
    }

    public static class CoreRepositoryExtension
    {
        /// <summary>
        /// Converts provided IQueryable of entities into a list of entities in an asynchronous manner,
        /// respecting the provided cancellation token.
        /// </summary>
        /// <typeparam name="T">The type of entities</typeparam>
        /// <param name="query">The IQueryable of entities to be converted into a list</param>
        /// <param name="cancellationToken">The cancellation token</param>
        /// <returns>A task that represents the asynchronous operation,
        /// with a return value of the list containing the entities</returns>
        public static async Task<ICollection<T>> ToCollectionAsync<T>(this IQueryable<T> query, CancellationToken cancellationToken)
        {
            return await query.ToListAsync(cancellationToken);
        }
    }
}