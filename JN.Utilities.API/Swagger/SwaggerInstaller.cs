using System;
using System.IO;
using System.Linq;
using System.Reflection;
using JN.Utilities.API.Helpers;
using JN.Utilities.API.ServiceInstaller;
using JN.Utilities.API.Swagger.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;

namespace JN.Utilities.API.Swagger
{
    public class SwaggerInstaller : IServiceInstaller
    {

        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {

            var swaggerConfig = configuration.GetSwaggerConfig("SwaggerOptions");

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", GetInfo(swaggerConfig));

                c.OperationFilter<ProblemDetailsFilter>();

                c.AddSecurityDefinition("basic", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "basic",
                    In = ParameterLocation.Header,
                    Description = "**Basic Authorization** - specify username and password.",
                });

                //load all xml comment files
                var executingAssemblyName = Assembly.GetExecutingAssembly().GetName().Name;

                if (!string.IsNullOrWhiteSpace(executingAssemblyName))
                {
                    var assemblyNamespace = executingAssemblyName.Split('.').First();

                    var pathToXmlDocumentsToLoad = AppDomain.CurrentDomain.GetAssemblies()
                        .Where(x => x.FullName != null && x.FullName.StartsWith(assemblyNamespace))
                        .Select(x => x.GetName().Name + ".xml")
                        .ToList();

                    pathToXmlDocumentsToLoad.ForEach(x =>
                    {
                        var fullPath = Path.Combine(AppContext.BaseDirectory, x);
                        if (File.Exists(fullPath))
                            c.IncludeXmlComments(fullPath, true);
                    });
                }



                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "basic"
                            }
                        },
                        Array.Empty<string>()
                    }
                });


                // Adds fluent validation rules to swagger
                c.AddFluentValidationRules();

            });
        }

        private static OpenApiInfo GetInfo(SwaggerOptionsConfig swaggerConfig)
        {
            return new OpenApiInfo
            {
                Version = "v1",
                Title = swaggerConfig.Title, 
                Description = swaggerConfig.Description,
                //TermsOfService = new Uri("https://example.com/terms"),
                Contact = new OpenApiContact
                {
                    Name = swaggerConfig.ContactName,
                    Email = swaggerConfig.ContactEmail,
                    Url = !string.IsNullOrWhiteSpace(swaggerConfig.ContactUrl)
                        ? new Uri(swaggerConfig.ContactUrl)
                        : null,
                },
                //License = new OpenApiLicense
                //{
                //    Name = "Use under LICX",
                //    Url = new Uri("https://example.com/license"),
                //}

            };
        }


    }
}