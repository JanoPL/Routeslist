using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using RoutesList.Interfaces;
using RoutesList.Services;
using RutesList.Gen;
using System;
using System.Collections.Generic;
using System.Text;

namespace RoutesList
{
    public static class RoutesListGen
    {
        public static IServiceCollection AddRouteList(
            this IServiceCollection services
        ) {
            services.AddTransient<IRoutes, Routes>();
            services.AddTransient<ITableBuilder, TableBuilder>();

            return services;
        }
    }
}
