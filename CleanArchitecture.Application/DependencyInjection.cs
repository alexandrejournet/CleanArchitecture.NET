using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CleanArchitecture.Application
{
    public static class DependencyInjection
    {
        public static void AddServices(this IServiceCollection services)
        {
            //Ajouter les services
            Assembly assembly = typeof(DependencyInjection).Assembly;

            assembly.GetTypes().Where(t => $"{assembly.GetName().Name}.Services" == t.Namespace
                                        && !t.IsAbstract
                                        && !t.IsInterface
                                        && t.Name.EndsWith("Service"))
            .Select(a => new { assignedType = a })
            .ToList()
            .ForEach(typesToRegister =>
            {
                services.AddScoped(typesToRegister.assignedType);
            });
        }
    }
}
