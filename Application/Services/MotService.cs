using CleanArchitecture.Application.Services.Interfaces;
using CleanArchitecture.Applications.Base;
using CleanArchitecture.Domain.Models;
using CleanArchitecture.Infrastructure.Interfaces;

namespace CleanArchitecture.Application.Services
{
    public class MotService : BaseService<IMotRepository, Mot>, IMotService
    {
        public MotService(IMotRepository repository) : base(repository)
        {
        }
    }
}
