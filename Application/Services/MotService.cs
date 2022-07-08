using CleanArchitecture.Applications.Base;
using CleanArchitecture.Infrastructure.Interfaces.Repository;
using CleanArchitecture.Infrastructure.Interfaces.Services;
using CleanArchitecture.Models;

namespace CleanArchitecture.Application.Services
{
    public class MotService : BaseService<IMotRepository, Mot>, IMotService
    {
        public MotService(IMotRepository repository) : base(repository)
        {
        }
    }
}
