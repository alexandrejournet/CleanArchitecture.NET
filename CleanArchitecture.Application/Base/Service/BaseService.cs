using CleanArchitecture.Domain.Utils;
using CleanArchitecture.Infrastructure.Base.Repository;
using CleanArchitecture.Infrastructure.Base.UnitOfWorks;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data.Common;
using System.Linq.Expressions;
using System.Security.Claims;

namespace CleanArchitecture.Application.Base.Service
{
    public class BaseService<TRepository, T> : IBaseService<T>
    where TRepository : IBaseRepository<T>
    where T : class
    {
        protected readonly TRepository _repository;

        /// <summary>
        /// Constructor for the Radiant service.
        /// </summary>
        /// <param name="repository">An instance of a repository derived from <see cref="ICoreRepository{T}"/>.</param>
        protected BaseService(IUnitOfWork unitOfWork)
        {
            _repository = unitOfWork.GetRepository<TRepository, T>();
        }

        public ClaimsPrincipal GetCurrentAuth()
        {
            return _repository.GetCurrentAuth();
        }

        /// <summary>
        /// Method to add new entity to the repository.
        /// </summary>
        /// <param name="entity">Entity of type T which need to be added.</param>
        /// <returns>Awaitable task.</returns>
        public async Task AddAsync(T entity)
        {
            await _repository.AddAsync(entity);
        }

        public async Task AddAsync<TEntity>(TEntity entity) where TEntity : class
        {
            await _repository.AddAsync(entity);
        }

        public async Task AddAndSaveAsync(T entity)
        {
            await _repository.AddAndSaveAsync(entity);
        }

        public async Task AddAndSaveAsync<TEntity>(TEntity entity) where TEntity : class
        {
            await _repository.AddAndSaveAsync(entity);
        }

        /// <summary>
        /// Method to add new range of entities to the repository.
        /// </summary>
        /// <param name="entities">Collection of entities of type T which need to be added.</param>
        /// <returns>Awaitable task.</returns>
        public async Task AddRangeAsync(ICollection<T> entities)
        {
            await _repository.AddRangeAsync(entities);
        }

        public async Task AddRangeAsync<TEntity>(ICollection<TEntity> entities) where TEntity : class
        {
            await _repository.AddRangeAsync(entities);
        }

        public async Task AddRangeAndSaveAsync(ICollection<T> entities)
        {
            await _repository.AddRangeAndSaveAsync(entities);
        }

        public async Task AddRangeAndSaveAsync<TEntity>(ICollection<TEntity> entities) where TEntity : class
        {
            await _repository.AddRangeAndSaveAsync(entities);
        }

        /// <summary>
        /// Checks if there are any entities that satisfy the provided condition.
        /// </summary>
        /// <param name="where">A delegate defining the condition for the entities to meet,</param>
        /// <returns>Bool value indicating whether any entities meet the specified condition.</returns>
        public async Task<bool> AnyAsyncBy(Expression<Func<T, bool>> where)
        {
            return await _repository.AnyAsync(where);
        }

        public async Task<bool> AnyAsyncBy<TEntity>(Expression<Func<TEntity, bool>> where) where TEntity : class
        {
            return await _repository.AnyAsync(where);
        }

        public async Task<bool> AnyAsyncBy<TEntity>(IQueryable<TEntity> queryable) where TEntity : class
        {
            return await _repository.AnyAsync(queryable);
        }

        /// <summary>
        /// Begins a new transaction in the context of the database.
        /// </summary>
        /// <returns>A task representing the asynchronous operation, containing the started database transaction.</returns>
        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _repository.BeginTransactionAsync();
        }

        /// <summary>
        /// Counts the total number of all entities present in the database.
        /// </summary>
        /// <returns>Task with the total count of entities available.</returns>
        public async Task<decimal> CountAllAsync()
        {
            return await _repository.CountAsync();
        }

        /// <summary>
        /// Counts the number of entities that satisfy the provided condition.
        /// </summary>
        /// <param name="where">A delegate defining the condition for the entities to meet.</param>
        /// <returns>Task with the total count of entities that meet the specified condition.</returns>
        public async Task<decimal> CountAllAsyncBy(Expression<Func<T, bool>> where)
        {
            return await _repository.CountAsync(where);
        }

        public async Task<decimal> CountAllAsyncBy<TEntity>(Expression<Func<TEntity, bool>> where) where TEntity : class
        {
            return await _repository.CountAsync(where);
        }

        public async Task<T> FindAsync(int id)
        {
            return await _repository.FindAsync(id);
        }

        public async Task<TEntity> FindAsync<TEntity>(int id) where TEntity : class
        {
            return await _repository.FindAsync<TEntity>(id);
        }

        /// <summary>
        /// Gets all entities that satisfy the provided condition.
        /// </summary>
        /// <param name="where">A delegate defining the condition for the entities to meet.</param>
        /// <returns>A task representing the asynchronous operation, containing the entities that meet the specified condition.</returns>
        public async Task<ICollection<T>> GetAllAsync(Expression<Func<T, bool>> where)
        {
            return await _repository.GetAllAsync(where);
        }

        /// <summary>
        /// Gets all entities satisfying the provided specification in the database.
        /// </summary>
        /// <param name="coreSpecifications">Specifications to be met by the entities.</param>
        /// <returns>Task representing the asynchronous operation, containing the list of entities that meet the provided specification.</returns>
        public async Task<ICollection<T>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        // Continue in this manner for the remaining methods...
        /// <summary>
        /// Gets all entities based on provided raw SQL query and specifications.
        /// </summary>
        /// <param name="query">Raw SQL query to be executed.</param>
        /// <param name="coreSpecifications">Specifications to be met by the entities.</param>
        /// <returns>A task representing the asynchronous operation, containing the list of entities that meet the provided query and specification.</returns>
        public async Task<ICollection<T>> GetAllAsync(string query)
        {
            return await _repository.GetAllAsync(query);
        }

        /// <summary>
        /// Gets all entities based on provided raw SQL query, parameters and specifications.
        /// </summary>
        /// <param name="query">Raw SQL query to be executed.</param>
        /// <param name="parameters">List of parameters to be used in query.</param>
        /// <param name="coreSpecifications">Specifications to be met by the entities.</param>
        /// <returns>A task representing the asynchronous operation, containing the list of entities that meet the provided query, parameters and specification.</returns>
        public async Task<ICollection<T>> GetAllAsync(string query, ICollection<DbParameter> parameters)
        {
            return await _repository.GetAllAsync(query, parameters);
        }

        public async Task<ICollection<TEntity>> GetAllAsync<TEntity>() where TEntity : class
        {
            return await _repository.GetAllAsync<TEntity>();
        }

        public async Task<ICollection<TEntity>> GetAllAsync<TEntity>(Expression<Func<TEntity, bool>> where) where TEntity : class
        {
            return await _repository.GetAllAsync(where);
        }

        public async Task<ICollection<TEntity>> GetAllAsync<TEntity>(string query) where TEntity : class
        {
            return await _repository.GetAllAsync<TEntity>(query);
        }

        public async Task<ICollection<TEntity>> GetAllAsync<TEntity>(string query, ICollection<DbParameter> parameters) where TEntity : class
        {
            return await _repository.GetAllAsync<TEntity>(query, parameters);
        }

        /// <summary>
        /// Gets all entities based on provided IQueryable query.
        /// </summary>
        /// <param name="query">IQueryable query to be executed.</param>
        /// <returns>A task representing the asynchronous operation, containing the list of entities that meet the provided query.</returns>
        public async Task<ICollection<TEntity>> GetAllByQueryable<TEntity>(IQueryable<TEntity> query)
        {
            return await _repository.GetAllByQueryable(query);
        }

        /// <summary>
        /// Gets an IQueryable of all entities.
        /// </summary>
        /// <returns>IQueryable of all entities.</returns>
        public IQueryable<T> GetAllQueryable()
        {
            return _repository.GetAllQueryable();
        }

        /// <summary>
        /// Gets entities by their specifications and provides a queryable list.
        /// </summary>
        /// <param name="where">A delegate defining the condition for the entities to meet.</param>
        /// <returns>IQueryable of entities that meet the specified condition.</returns>
        public IQueryable<T> GetAllQueryable(Expression<Func<T, bool>> where)
        {
            return _repository.GetAllQueryable(where);
        }

        /// <summary>
        /// Gets entities based on provided raw SQL query and specifications.
        /// </summary>
        /// <param name="query">Raw SQL query to be executed.</param>
        /// <param name="coreSpecifications">Specifications to be met by the entities.</param>
        /// <returns>A IQueryable representation of entities that meet the provided query and specification.</returns>
        public IQueryable<T> GetAllQueryable(string query)
        {
            return _repository.GetAllQueryable(query);
        }

        /// <summary>
        /// Gets entities based on provided raw SQL query, parameters and specifications.
        /// </summary>
        /// <param name="query">Raw SQL query to be executed.</param>
        /// <param name="parameters">List of parameters to be used in the query.</param>
        /// <param name="coreSpecifications">Specifications to be met by the entities.</param>
        /// <returns>A IQueryable representation of entities that meet the provided query, parameters and specification.</returns>
        public IQueryable<T> GetAllQueryable(string query, ICollection<DbParameter> parameters)
        {
            return _repository.GetAllQueryable(query, parameters);
        }

        /// <summary>
        /// Get an entity by the given specifications.
        /// </summary>
        /// <param name="where">A delegate defining the condition for the entity to meet.</param>
        /// <returns>A task representing the asynchronous operation, containing an entity that meets the provided condition.</returns>
        public async Task<T?> GetBy(Expression<Func<T, bool>> where)
        {
            return await _repository.GetBy(where);
        }


        public async Task<TEntity?> GetBy<TEntity>(Expression<Func<TEntity, bool>> where) where TEntity : class
        {
            return await _repository.GetBy(where);
        }

        public async Task<TEntity?> GetBy<TEntity>(string query, ICollection<DbParameter> parameters) where TEntity : class
        {
            return await _repository.GetBy<TEntity>(query, parameters);
        }

        public async Task<Paged<T>> GetPagedResult(Page page)
        {
            return await _repository.GetPagedResult(page);
        }

        public async Task<Paged<T>> GetPagedResult(Page page, string query, ICollection<DbParameter> parameters)
        {
            return await _repository.GetPagedResult(page, query, parameters);
        }

        public async Task<Paged<TEntity>> GetPagedResult<TEntity>(Page page) where TEntity : class
        {
            return await _repository.GetPagedResult<TEntity>(page);
        }

        public async Task<Paged<TEntity>> GetPagedResult<TEntity>(Page page, string query, ICollection<DbParameter> parameters) where TEntity : class
        {
            return await _repository.GetPagedResult<TEntity>(page, query, parameters);
        }

        /// <summary>
        /// Gets entities based on provided page and specifications.
        /// </summary>
        /// <param name="page">Defines the page boundaries.</param>
        /// <param name="coreSpecifications">Specifications to be met by the entities.</param>
        /// <returns>A task with a collection of all entities meeting the provided page and specifications.</returns>
        public Task<ICollection<T>> PageAllAsync(Page page)
        {
            return _repository.PageAllAsync(page);
        }

        /// <summary>
        /// Enables paging to the queryable list of represented entities based on provided page and specifications.
        /// </summary>
        /// <param name="page">Defines the page boundaries.</param>
        /// <param name="coreSpecifications">Specifications to be met by the entities.</param>
        /// <returns>A IQueryable sublist of all entities meeting the provided page and specifications.</returns>
        public IQueryable<T> PageAllQueryable(Page page)
        {
            return _repository.PageAllQueryable(page);
        }

        /// <summary>
        /// Deletes the entity from the database.
        /// </summary>
        /// <param name="entity">The entity to be removed.</param>
        /// <returns>Awaitable task.</returns>
        public void Remove(T entity)
        {
            _repository.Remove(entity);
        }

        public void Remove<TEntity>(TEntity entity) where TEntity : class
        {
            _repository.Remove(entity);
        }

        public async Task RemoveAndSaveAsync(T entity)
        {
            await _repository.RemoveAndSaveAsync(entity);
        }

        public async Task RemoveAndSaveAsync<TEntity>(TEntity entity) where TEntity : class
        {
            await _repository.RemoveAndSaveAsync(entity);
        }

        /// <summary>
        /// Deletes a range of entities from the database.
        /// </summary>
        /// <param name="entities">The entities to be removed.</param>
        /// <returns>Awaitable task.</returns>
        public void RemoveRange(ICollection<T> entities)
        {
            _repository.RemoveRange(entities);
        }

        public void RemoveRange<TEntity>(ICollection<TEntity> entities) where TEntity : class
        {
            _repository.RemoveRange(entities);
        }

        public async Task RemoveRangeAndSaveAsync(ICollection<T> entities)
        {
            await _repository.RemoveRangeAndSaveAsync(entities);
        }

        public async Task RemoveRangeAndSaveAsync<TEntity>(ICollection<TEntity> entities) where TEntity : class
        {
            await _repository.RemoveRangeAndSaveAsync(entities);
        }

        /// <summary>
        /// Save any changes in the context to the database.
        /// </summary>
        /// <returns>Awaitable task.</returns>
        public async Task SaveAsync()
        {
            await _repository.SaveChangesAsync();
        }

        /// <summary>
        /// Updates an entity in the database.
        /// </summary>
        /// <param name="entity">Entity to be updated.</param>
        /// <returns>Awaitable task.</returns>
        public void Update(T entity)
        {
            _repository.Update(entity);
        }

        public void Update<TEntity>(TEntity entity) where TEntity : class
        {
            _repository.Update(entity);
        }

        public async Task UpdateAndSaveAsync(T entity)
        {
            await _repository.UpdateAndSaveAsync(entity);
        }

        public async Task UpdateAndSaveAsync<TEntity>(TEntity entity) where TEntity : class
        {
            await _repository.UpdateAndSaveAsync(entity);
        }

        /// <summary>
        /// Method to update a range of entities in the repository.
        /// </summary>
        /// <param name="entities">Collection of entities which need to be updated.</param>
        /// <returns>Awaitable task.</returns>
        public void UpdateRange(ICollection<T> entities)
        {
            _repository.UpdateRange(entities);
        }

        public void UpdateRange<TEntity>(ICollection<TEntity> entities) where TEntity : class
        {
            _repository.UpdateRange(entities);
        }

        public async Task UpdateRangeAndSaveAsync(ICollection<T> entities)
        {
            await _repository.UpdateRangeAndSaveAsync(entities);
        }

        public async Task UpdateRangeAndSaveAsync<TEntity>(ICollection<TEntity> entities) where TEntity : class
        {
            await _repository.UpdateRangeAndSaveAsync(entities);
        }
    }
}
