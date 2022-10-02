using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CleanArchitecture.Application
{
    public static class DependencyInjection
    {
        public static void AddServices(this IServiceCollection services)
        {
            //Ajouter les services
            Assembly assembly = Assembly.GetExecutingAssembly();

            assembly.GetTypes().Where(t => $"{assembly.GetName().Name}.Services" == t.Namespace
                                        && !t.IsAbstract
                                        && !t.IsInterface
                                        && t.Name.EndsWith("Service"))
            .Select(a => new { assignedType = a, serviceTypes = a.GetInterfaces().ToList() })
            .ToList()
            .ForEach(typesToRegister =>
            {
                typesToRegister.serviceTypes.ForEach(typeToRegister => services.AddScoped(typeToRegister, typesToRegister.assignedType));
            });
        }
    }
}
