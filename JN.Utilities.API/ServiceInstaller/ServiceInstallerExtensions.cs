using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JN.Utilities.API.ServiceInstaller
{
    public static class ServiceInstallerExtensions
    {
        /// <summary>
        /// Install all services in classes that implement <see cref="IServiceInstaller"/>
        /// </summary>
        /// <typeparam name="T">Type from which assembly the services will be installed.</typeparam>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void InstallServicesInAssembly<T>(this IServiceCollection services, IConfiguration configuration)
        {
            var installers = typeof(T).Assembly.ExportedTypes
                .Where(x =>
                    typeof(IServiceInstaller).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .Select(Activator.CreateInstance)
                .Cast<IServiceInstaller>()
                .ToList();

            installers.ForEach(installer => installer.InstallServices(services, configuration));
        }
    }
}
