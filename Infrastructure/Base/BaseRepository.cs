using Microsoft.EntityFrameworkCore;
using CleanArchitecture.Models.Base;
using System.Linq.Expressions;
using CleanArchitecture.Infrastructure.Database;
using CleanArchitecture.Infrastructure.Interfaces;

namespace CleanArchitecture.Infrastructure.Base
{
    public class BaseRepository<TEntity, TContext> : IBaseRepository<TEntity> 
        where TEntity : BaseEntity
        where TContext : CoreDbContext
    {
        protected readonly TContext _dbContext;

        public BaseRepository(TContext context)
        {
            _dbContext = context;
        }

        public async Task<TEntity> GetById(int id)
        {
            try
            {
                return await _dbContext.Set<TEntity>().FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Impossible de récupérer l'entité: {ex.Message}");
            }
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            try
            {
                return await _dbContext.Set<TEntity>().AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Impossible de récupérer les entités: {ex.Message}");
            }
        }

        public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> where)
        {
            try
            {
                return await _dbContext.Set<TEntity>().Where(where).AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Impossible de récupérer les entités: {ex.Message}");
            }
        }
    }
}
