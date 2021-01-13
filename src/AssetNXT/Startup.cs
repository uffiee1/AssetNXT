using System;
using AssetNXT.Hubs;
using AssetNXT.Models.Data;
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
            ConfigureSwaggerServices(services);
            ConfigureDatabaseServices(services);
            ConfigureSpaFilesServices(services);
            ConfigureCrossOriginResourceSharing(services);

            // Scope
            // XXX: Replace Singleton by Scoped or Transient
            // XXX: Must first resolve issues with SignalR and MockRuuviStationRepository
            // to avoid creating mutiple background workers that are all uploading to the SingalR Hub simultaneously

            // AAA: When we make the parent service a singleton, that means that the child service is unable to be created per page load.
            // In our case IMongoDataRepository as a parent is used as a SignalR, Mock and ServiceConfigurations instances.
            // AAA: So, Does the solution require to use Transient for the SignalR?
            // AAA: It’s still going to give us some unintended behaviour because the transient child is not going to be created “everytime” as we might first
            // think. Sure, it will be created everytime it’s “requested”, but that will only be once (for the parent).

            // I think the logic behind this not throwing an exception, but scoped will, is that transient is “everytime this service is requested, create a new
            // instance”, so technically this is correct behaviour (Even though it’s likely to cause issues)

            services.AddSingleton(typeof(IMongoDataRepository<>), typeof(MongoDataRepository<>));
            services.AddSingleton(typeof(IMongoDataRepository<RuuviStation>), typeof(MockRuuviStationRepository));

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    var contractResolver = new CamelCasePropertyNamesContractResolver();
                    options.SerializerSettings.ContractResolver = contractResolver;
                });

            // Controllers Serialization
            services.AddControllers().AddNewtonsoftJson(s => { s.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver(); });

            // SignalR
            services.AddSignalR();
        }

        public void ConfigureSpaFilesServices(IServiceCollection services)
        {
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "../AssetNXT.Client/build";
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

        public void ConfigureCrossOriginResourceSharing(IServiceCollection services)
        {
            // SignalR
            services.AddCors(options =>
            {
                options.AddPolicy("ClientPermission", policy =>
                {
                    policy.SetIsOriginAllowed(origin => true)
                     .AllowAnyHeader()
                     .AllowAnyMethod()
                     .AllowCredentials();
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // SignalR
            app.UseCors("ClientPermission");

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<RuuviStationHub>("/livestations");
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
