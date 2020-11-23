using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JN.Utilities.API.ServiceInstaller
{
    public interface IServiceInstaller
    {
        void InstallServices(IServiceCollection services, IConfiguration configuration);
    }
}
