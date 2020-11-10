using System;

using AssetNXT.Repository;
using AssetNXT.Settings;

using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;

namespace AssetNXT
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    var contractResolver = new CamelCasePropertyNamesContractResolver();
                    options.SerializerSettings.ContractResolver = contractResolver;
                });

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "../AssetNXT.Client/build";
            });

            ConfigureSwaggerServices(services);
            ConfigureDatabaseServices(services);

            // Scope
            services.AddScoped(typeof(IMongoDataRepository<>), typeof(MongoDataRepository<>));
            /* services.AddScoped(typeof(IMongoDataRepository<>), typeof(MockDataRepository<>)); */

            // Controllers Serialization
            services.AddControllers().AddNewtonsoftJson(s => { s.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver(); });
        }

        public void ConfigureSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "API",
                    Version = "v1"
                });
            });
        }

        public void ConfigureDatabaseServices(IServiceCollection services)
        {
            // MongoDB Configuration
            services.Configure<MongoDbSettings>(Configuration.GetSection(nameof(MongoDbSettings)));
            services.AddSingleton<IMongoDbSettings>(serviceProvider => serviceProvider.GetRequiredService<IOptions<MongoDbSettings>>().Value);
        }

        public void ConfigureSwaggerServices(IServiceCollection services)
        {
            // Swagger
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "API",
                    Version = "v1"
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Client SPA
            app.UseSpaStaticFiles();
            app.UseStaticFiles();

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "../AssetNXT.Client";
                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });

            // Swagger config
            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "API");
            });
        }
    }
}
