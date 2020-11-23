using FluentValidation.AspNetCore;
using JN.Utilities.API.ApiConfiguration;
using JN.Utilities.API.ServiceInstaller;
using JN.Utilities.Contracts.V1.Requests;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;

namespace JN.Utilities.API.ServicesInstallers
{
    public class SystemServicesInstaller: IServiceInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers()
                // add support for Newtonsoft JSON for controllers
                .AddNewtonsoftJson(jsonOptions =>
                {
                    jsonOptions.SerializerSettings.ContractResolver =
                        new CamelCasePropertyNamesContractResolver();
                })
                .CustomConfigureApiBehaviorOptions()
                .AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<ProblemDefinition>())

                .CustomConfigureApiBehaviorOptions();


            
            // add support for Newtonsoft JSON in swagger
            services.AddSwaggerGenNewtonsoftSupport();

            
        }
    }
}
