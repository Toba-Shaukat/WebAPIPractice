using AWebAPIPractice;
using AWebAPIPractice.Controllers;
using Contracts;
using Entities.Context;
using LoggerService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repository;

namespace WebAPIPractice.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", cfg => cfg.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            }); 
        }
        public static void ConfigureIISIntegration(this IServiceCollection services)
        {
            services.Configure<IISOptions>(options => { });
        }
        public static void ConfigureLoggerService(this IServiceCollection services)
        {
            services.AddScoped<ILoggerManager, LoggerManager>();
        }
        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<RepositoryContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("CS"), cfg => cfg.MigrationsAssembly("AWebAPIPractice"));
            });
        }
        public static void ConfigureRepositoryManager(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryManager, RepositoryManager>();
        }

        public static IMvcBuilder AddCustomCSVFormatter(this IMvcBuilder mvcBuilder)
        {
            return mvcBuilder.AddMvcOptions(config => config.OutputFormatters.Add(new CsvOutputFormatter()));
        }
        public static void ConfigureVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true; //api/employees 
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ApiVersionReader = new HeaderApiVersionReader("arvinder-api-version");
                options.Conventions.Controller<CompaniesController>().HasApiVersion(new ApiVersion(1, 0));
                options.Conventions.Controller<CompaniesV2Controller>().HasDeprecatedApiVersion(new ApiVersion(2, 0));
            });
        }
    }
}
