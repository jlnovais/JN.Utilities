using System;
using AutoMapper;
using JN.Utilities.API.ApiConfiguration;
using JN.Utilities.API.Helpers;
using JN.Utilities.API.ServiceInstaller;
using JN.Utilities.API.Swagger;
using JN.Utilities.Core.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace JN.Utilities.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.InstallServicesInAssembly<Startup>(Configuration);
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, 
            ILoggerFactory loggerFactory,
            IServiceProvider serviceProvider
            )
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}


            app.UseCustomExceptionHandler(loggerFactory, !env.IsDevelopment());

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.ConfigSwagger(
                Configuration.GetSwaggerConfig("SwaggerOptions"),
                () => GetType().Assembly.GetManifestResourceStream("JN.Utilities.API.EmbeddedAssets.index.html")
            );

            app.UseStaticFiles();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            serviceProvider.GetService<IProblemSolutionRepository>().Setup();

        }
    }
}
