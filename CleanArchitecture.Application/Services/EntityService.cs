using CleanArchitecture.Application.Base;
using CleanArchitecture.Domain.BO;
using CleanArchitecture.Infrastructure.Repository;

namespace CleanArchitecture.Application.Services
{
    public class EntityService : BaseService<EntityRepository, Entity>
    {
        public EntityService(EntityRepository repository) : base(repository)
        {
        }
    }
}
