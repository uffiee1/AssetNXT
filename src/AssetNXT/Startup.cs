using System;

using AssetNXT.Repositories;
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
        private IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get => _configuration; set => _configuration = value; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Controllers Serialization
            services.AddControllers().AddNewtonsoftJson(s => { s.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver(); });

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // Scope CHANGE ME HERE
            services.AddScoped(typeof(IMongoDataRepository<>), typeof(MockDataRepository<>));
            //services.AddScoped(typeof(IMongoDataRepository<>), typeof(MockDataRepository<>));

            // MongoDb Configurations
            services.Configure<MongoDbSettings>(Configuration.GetSection(nameof(MongoDbSettings)));

            // Provider
            services.AddSingleton<IMongoDbSettings>(serviceProvider => serviceProvider.GetRequiredService<IOptions<MongoDbSettings>>().Value);

            // Swagger
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Ruuvi Rest API",
                    Version = "v1"
                });
            });

            // React js Start-up Configurations
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "../AssetNXT.Client/build";
            });
        }

        private void ConfigureDatabaseServices(IServiceCollection services)
        {
            // MongoDB Section
            var mongoDbSectionName = nameof(MongoDbSettings);
            var mongoDbSection = Configuration.GetSection(mongoDbSectionName);

            // MongoDB Configuration
            services.Configure<MongoDbSettings>(mongoDbSection);
            services.AddSingleton<IMongoDbSettings>(serviceProvider =>
                serviceProvider.GetRequiredService<IOptions<MongoDbSettings>>().Value);

            // MongoDB Repositories
            services.AddSingleton(typeof(IMongoDataRepository<>), typeof(MongoDataRepository<>));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseSpaStaticFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            // Client SPA
            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "../AssetNXT.Client";
                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });

            // Swagger config
            app.UseSwagger(options =>
            {
                options.RouteTemplate = "swagger/{documentname}/swagger.json";
            });

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("swagger/v1/swagger.json", "Ruuvi Rest API");
            });
        }
    }
}
