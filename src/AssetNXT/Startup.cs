using System;
using System.Text.Json;

using AssetNXT.Repositories;
using AssetNXT.Services;
using AssetNXT.Settings;

using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace AssetNXT
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

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
            services.AddSingleton<IRuuviStationService, MockRuuviStationService>();
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
        }
    }
}
