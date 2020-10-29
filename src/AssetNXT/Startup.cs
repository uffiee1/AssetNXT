using System;
using System.Text.Json;

using AssetNXT.Repositories;
using AssetNXT.Services;
using AssetNXT.Settings;

using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

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
            services.AddControllersWithViews()
                .AddJsonOptions(options => options.JsonSerializerOptions
                .PropertyNamingPolicy = JsonNamingPolicy.CamelCase);

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "../AssetNXT.Client/build";
            });

            ConfigureDatabaseServices(services);

            // MongoDb Configurations
            services.Configure<MongoDbSettings>(Configuration.GetSection(nameof(MongoDbSettings)));

            services.AddSingleton<IMongoDbSettings>(serviceProvider =>
                serviceProvider.GetRequiredService<IOptions<MongoDbSettings>>().Value);

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // services.AddScoped(typeof(IMongoRepository<>), typeof(MongoRepository<>));

            // services.AddSingleton<IRuuviStationService, MockRuuviStationService>();
            services.AddSingleton<IRuuviStationService, MongoRuuviStationService>();

            // Swagger
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v", new Microsoft.OpenApi.Models.OpenApiInfo
                    {
                        Title = "Ruuvi Rest API",
                        Version = "v1"
                    });
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

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "../AssetNXT.Client";
                if (env.IsDevelopment())
                {
                    spa.UseReactDevelopmentServer(npmScript: "start");
                }
            });

            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Ruuvi Rest API");
            });
        }
    }
}
