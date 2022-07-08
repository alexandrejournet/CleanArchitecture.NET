using CleanArchitecture.Infrastructure.Interfaces.Repository;
using CleanArchitecture.Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            //Ajouter les repository
            services.AddScoped<IMotRepository, MotRepository>();
        }
    }
}
