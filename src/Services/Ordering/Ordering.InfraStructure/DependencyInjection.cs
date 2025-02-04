using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Application.Data;
using Ordering.InfraStructure.Data.Interceptors;

namespace Ordering.InfraStructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfratructureServices(this IServiceCollection services, 
                                                                  IConfiguration configuration)
        {
            // add database services
            var connectionString = configuration.GetConnectionString("Database");


            services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
            services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

            services.AddDbContext<ApplicationDbContext>((sp, options) =>
            {
                options.UseSqlServer(connectionString);
                options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
            });

            services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

            return services;
        }
    }
}
