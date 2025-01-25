namespace Ordering.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAPIServices(this IServiceCollection services)
        {

            // add carter services

            return services;
        }


        public static WebApplication UseApiServices(this WebApplication app)
        {

            // app.MapCarter();

            return app;
        }
    }
}
