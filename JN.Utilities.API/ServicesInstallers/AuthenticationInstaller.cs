using System.Text;
using JN.Authentication.Interfaces;
using JN.Authentication.Scheme;
using JN.Utilities.API.AuthorizationHandlers;
using JN.Utilities.API.ServiceInstaller;
using JN.Utilities.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JN.Utilities.API.ServicesInstallers
{
    public class AuthenticationInstaller : IServiceInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {

            // Basic authentication 
            services.AddAuthentication(BasicAuthenticationDefaults.AuthenticationScheme)
                .AddBasic(options =>
                {
                    options.Realm = "JNApi";
                    options.LogInformation = true; //optional, default is false;
                    options.HttpPostMethodOnly = false;
                    options.HeaderEncoding = Encoding.UTF8; //optional, default is UTF8;
                    options.ChallengeResponse = BasicValidationService.ChallengeResponse;
                });

            // validation service
            services.AddTransient<IBasicValidationService, BasicValidationService>();

            // Add custom authorization handlers
            services.AddAuthorization(options =>
            {
                options.AddPolicy("OptimizationAccess", policy => policy.Requirements.Add(new GenericAccessRequirement(ConstantsAuthentication.UserRoles.Optimization.ToString())));
                options.AddPolicy("OptimizationReaderAccess", policy => policy.Requirements.Add(new GenericAccessRequirement(ConstantsAuthentication.UserRoles.OptimizationReader.ToString())));
            });

            services.AddSingleton<IAuthorizationHandler, CustomAuthorizationHandler>();
            /*Authorization using custom policies - end*/

        }
    }
}