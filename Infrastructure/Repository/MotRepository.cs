using CleanArchitecture.Infrastructure.Base;
using CleanArchitecture.Infrastructure.Database;
using CleanArchitecture.Infrastructure.Interfaces.Repository;
using CleanArchitecture.Models;

namespace CleanArchitecture.Infrastructure.Repository
{
    public class MotRepository : BaseRepository<Mot, CoreDbContext>, IMotRepository
    {
        public MotRepository(CoreDbContext databaseContext) : base(databaseContext)
        {
        }
    }
}
