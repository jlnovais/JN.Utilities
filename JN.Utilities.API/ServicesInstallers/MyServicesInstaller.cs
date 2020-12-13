using JN.Utilities.API.Helpers;
using JN.Utilities.API.ServiceInstaller;
using JN.Utilities.Core.Repositories;
using JN.Utilities.Core.Services;
using JN.Utilities.Repositories;
using JN.Utilities.Repositories.SQLite;
using JN.Utilities.Services;
using JN.Utilities.Services.Dto;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JN.Utilities.API.ServicesInstallers
{
    public class MyServicesInstaller: IServiceInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ISolverService, SolverService>();

            services.AddScoped<IUsersService>(provider => GetIUsersService(configuration));

            services.AddSingleton(new ProblemSolutionServiceConfig() { SolutionsTTLMinutes  = configuration.GetInt("Solutions_TTL_minutes") });
            services.AddScoped<IProblemSolutionService, ProblemSolutionService>();
            services.AddSingleton<IProblemSolutionRepository>(new ProblemSolutionRepository(configuration.GetConnectionString("DatabaseName")));


        }

        private IUsersService GetIUsersService(IConfiguration configuration)
        {
            var users = configuration.GetUsersConfig("AllowedUsers");
            return new UsersService(users);
        }
    }
}
