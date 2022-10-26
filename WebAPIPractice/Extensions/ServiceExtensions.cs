using AWebAPIPractice;
using Contracts;
using Entities.Context;
using LoggerService;
using Microsoft.AspNetCore.Builder;
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
    }
}
