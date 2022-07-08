using CleanArchitecture.Application.Services;
using CleanArchitecture.Infrastructure.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Application
{
    public static class DependencyInjection
    {
        public static void AddServices(this IServiceCollection services)
        {
            //Ajouter les services
            services.AddScoped<IMotService, MotService>();
        }
    }
}
