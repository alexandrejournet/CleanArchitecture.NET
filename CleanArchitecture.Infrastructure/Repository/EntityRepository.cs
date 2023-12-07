using CleanArchitecture.Domain.BO;
using CleanArchitecture.Infrastructure.Base;
using CleanArchitecture.Infrastructure.Database;
using Microsoft.AspNetCore.Http;

namespace CleanArchitecture.Infrastructure.Repository
{
    public class EntityRepository : BaseRepository<Entity, CoreDbContext>
    {
        public EntityRepository(CoreDbContext databaseContext, IHttpContextAccessor httpContext) : base(databaseContext, httpContext)
        {
        }
    }
}
