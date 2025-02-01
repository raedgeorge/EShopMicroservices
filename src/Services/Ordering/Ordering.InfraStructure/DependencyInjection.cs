using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
                options.AddInterceptors(new AuditableEntityInterceptor());
            });

            return services;
        }
    }
}
