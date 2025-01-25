using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ordering.InfraStructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfratructureServices(this IServiceCollection services, 
                                                                  IConfiguration configuration)
        {
            // add database services
            var connectionString = configuration.GetConnectionString("Database");

            return services;
        }
    }
}
