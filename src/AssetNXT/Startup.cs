using System;
using AssetNXT.Configuration;
using AssetNXT.Models.Data;
using AssetNXT.Profiles;
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
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // MongoDb Configurations
            services.Configure<MongoDbSettings>(Configuration.GetSection(nameof(MongoDbSettings)));

            // Provider
            services.AddSingleton<IMongoDbSettings>(serviceProvider => serviceProvider.GetRequiredService<IOptions<MongoDbSettings>>().Value);

            // Mapping
            // https://stackoverflow.com/questions/40275195/how-to-set-up-automapper-in-asp-net-core
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // Scope
            services.AddScoped(typeof(IMongoDataRepository<>), typeof(MongoDataRepository<>));
            /* services.AddScoped(typeof(IMongoDataRepository<>), typeof(MockDataRepository<>)); */

            // Controllers Serialization
            services.AddControllers().AddNewtonsoftJson(s => { s.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver(); });

            // Swagger
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "API",
                    Version = "v1"
                });
            });

            // React js Start-up Configurations
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "../AssetNXT.Client/build";
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
