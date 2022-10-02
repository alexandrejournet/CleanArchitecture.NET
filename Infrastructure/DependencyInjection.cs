using CleanArchitecture.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CleanArchitecture.Infrastructure
{
    public static class DependencyInjection
    {

        public static void AddDatabaseContext(this IServiceCollection services, string config)
        {
            // Récupérer la config du appsettings depuis builder.Configuration dans Program.cs
            #region MySQL
            services.AddDbContext<CoreDbContext>(options =>
                {
                    options.EnableSensitiveDataLogging(true);
                    options.UseMySql(config, ServerVersion.AutoDetect(config));
                }, ServiceLifetime.Transient); 
            #endregion

            services.BuildServiceProvider().GetService<CoreDbContext>().Database.Migrate();
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            //Ajouter les repository
            Assembly assembly = Assembly.GetExecutingAssembly();

            assembly.GetTypes().Where(t => $"{assembly.GetName().Name}.Repositories" == t.Namespace
                                        && !t.IsAbstract
                                        && !t.IsInterface
                                        && t.Name.EndsWith("Repository"))
            .Select(a => new { assignedType = a, serviceTypes = a.GetInterfaces().ToList() })
            .ToList()
            .ForEach(typesToRegister =>
            {
                typesToRegister.serviceTypes.ForEach(typeToRegister => services.AddScoped(typeToRegister, typesToRegister.assignedType));
            });
        }
    }
}
