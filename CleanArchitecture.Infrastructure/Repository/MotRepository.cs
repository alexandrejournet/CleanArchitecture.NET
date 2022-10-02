using CleanArchitecture.Domain.Models;
using CleanArchitecture.Infrastructure.Base;
using CleanArchitecture.Infrastructure.Database;
using CleanArchitecture.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;

namespace CleanArchitecture.Infrastructure.Repository
{
    public class MotRepository : BaseRepository<Mot, CoreDbContext>, IMotRepository
    {
        public MotRepository(CoreDbContext databaseContext, IHttpContextAccessor httpContext) : base(databaseContext, httpContext)
        {
        }
    }
}
