using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using RoutesList.Build.Models;
using RoutesList.Gen.Extensions;

namespace RoutesList.Gen
{
    public static class RoutesListView
    {
        private static IApplicationBuilder UseRoutesList(
            this IApplicationBuilder app,
            RoutesListOptions options)
        {
            return app;
        }

        public static IApplicationBuilder UseRoutesList(
            this IApplicationBuilder app,
            Action<RoutesListOptions> routesListOptions)
        {
            RoutesListOptions options;
            using (var scope = app.ApplicationServices.CreateScope()) {
                options = scope.ServiceProvider.GetRequiredService<IOptionsSnapshot<RoutesListOptions>>().Value;
                routesListOptions?.Invoke(options);
            }

            app.UseEndpoints(endpoints => {
                endpoints.MapRouteList($"{options.Endpoint}", options);
                endpoints.MapRouteList($"{options.Endpoint}/json", options);
            });

            return app.UseRoutesList(options);
        }

        public static IApplicationBuilder UseRoutesList(this IApplicationBuilder app)
        {
            RoutesListOptions options;

            using (var scope = app.ApplicationServices.CreateScope()) {
                options = scope.ServiceProvider.GetRequiredService<IOptionsSnapshot<RoutesListOptions>>().Value;
            }

            app.UseEndpoints(endpoints => {
                endpoints.MapRouteList($"{options.Endpoint}", options);
                endpoints.MapRouteList($"{options.Endpoint}/json", options);
            });

            return app.UseRoutesList(options);
        }
    }
}
