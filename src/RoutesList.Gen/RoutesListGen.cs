using Microsoft.Extensions.DependencyInjection;
using RoutesList.Build.Interfaces;
using RoutesList.Build.Services;
using RoutesList.Build.Services.StaticFileBuilder;
using RoutesList.Services;

namespace RoutesList.Gen
{
    /// <summary>
    /// Provides extension methods for registering RoutesList services in the dependency injection container.
    /// </summary>
    public static class RoutesListGen
    {
        /// <summary>
        /// Registers all required RoutesList services in the dependency injection container.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
        /// <returns>The same service collection for chaining.</returns>
        public static IServiceCollection AddRoutesList(
            this IServiceCollection services
        )
        {
            services.AddTransient<IRoutes, Routes>();
            services.AddTransient<ITableBuilder, TableBuilder>();
            services.AddSingleton<IBuilder, Builder>();

            return services;
        }
    }
}