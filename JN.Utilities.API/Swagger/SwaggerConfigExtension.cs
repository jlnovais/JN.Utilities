using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.OpenApi.Models;

namespace JN.Utilities.API.Swagger
{
    public static class SwaggerConfigExtension
    {
        public static IApplicationBuilder ConfigSwagger(this IApplicationBuilder app, SwaggerOptionsConfig config,
            Func<Stream> customPageLoader = null)
        {

            //https://docs.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-3.1&tabs=visual-studio
            // Enable middleware to serve generated Swagger as a JSON endpoint.

            app.UseSwagger(option =>
                {
                    option.RouteTemplate = config.JsonRoute;

                    option.PreSerializeFilters.Add((swaggerDoc, httpRequest) =>
                    {
                        if (!httpRequest.Headers.ContainsKey("X-Forwarded-Host")) return;

                        var serverUrl = $"{httpRequest.Headers["X-Forwarded-Proto"]}://" +
                                        $"{httpRequest.Headers["X-Forwarded-Host"]}/" +
                                        $"{httpRequest.Headers["X-Forwarded-Prefix"]}";

                        swaggerDoc.Servers = new List<OpenApiServer>()
                        {
                            new OpenApiServer { Url = serverUrl }
                        };
                    });


                }
            );

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.InjectStylesheet("css/customcss.css");

                if (customPageLoader != null)
                    c.IndexStream = customPageLoader;

                c.SwaggerEndpoint(config.UiEndpoint, config.DefinitionDescription);

                //To serve the Swagger UI at the app's root set the RoutePrefix property to an empty string
                c.RoutePrefix = string.Empty;
                
                c.EnableFilter();
                c.DisplayRequestDuration();

            });

            return app;
        }

 
    }
}
