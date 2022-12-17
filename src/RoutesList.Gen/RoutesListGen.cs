using System;
using Microsoft.Extensions.DependencyInjection;
using RoutesList.Build.Services.StaticFileBuilder;
using RoutesList.Interfaces;
using RoutesList.Services;

namespace RoutesList.Gen
{
    public static class RoutesListGen
    {
        public static IServiceCollection AddRoutesList(
            this IServiceCollection services
        ) {
            services.AddTransient<IRoutes, Routes>();
            services.AddTransient<ITableBuilder, TableBuilder>();
            services.AddSingleton<IBuilder, Builder>();

            return services;
        }
    }
}
