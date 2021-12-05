using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RoutesList.Gen.Extensions;
using System;

namespace RoutesList.Gen
{
    public static class RoutesListView
    {
        public static IApplicationBuilder UseRoutesList(
            this IApplicationBuilder app,
            RoutesListOptions options)
        {
            return app;
        }

        public static IApplicationBuilder UseRoutesList(
            this IApplicationBuilder app,
            Action<RoutesListOptions> routesListOptions = null)
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
    }
}
