using System;
using Microsoft.Extensions.DependencyInjection;
using RoutesList.Build.Services.StaticFileBuilder;
using RoutesList.Interfaces;
using RoutesList.Services;

namespace RoutesList.Gen
{
    public static class RoutesListGen
    {
        [Obsolete ("Method AddRouteList() is deprecated, please use AddRoutesList(). The method AddRouteList() it will be remove in next version 0.2.4", true)]
        public static IServiceCollection AddRouteList(
            this IServiceCollection services
        )
        {
            services.AddTransient<IRoutes, Routes>();
            services.AddTransient<ITableBuilder, TableBuilder>();
            services.AddSingleton<IBuilder, Builder>();

            return services;
        }

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
