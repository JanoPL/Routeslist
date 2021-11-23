using Microsoft.Extensions.DependencyInjection;
using RoutesList.Interfaces;
using RoutesList.Services;

namespace RoutesList.Gen
{
    public static class RoutesListGen
    {
        public static IServiceCollection AddRouteList(
            this IServiceCollection services
        )
        {
            services.AddTransient<IRoutes, Routes>();
            services.AddTransient<ITableBuilder, TableBuilder>();

            return services;
        }
    }
}
