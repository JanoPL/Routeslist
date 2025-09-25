using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using RoutesList.Build.Models;
using RoutesList.Gen.Extensions;

namespace RoutesList.Gen
{
    /// <summary>
    /// Provides extension methods for configuring and using RoutesList in ASP.NET Core applications.
    /// </summary>
    public static class RoutesListView
    {
        /// <summary>
        /// Adds RoutesList middleware to the application pipeline with custom configuration options.
        /// </summary>
        /// <param name="app">The application builder instance.</param>
        /// <param name="routesListOptions">Action to configure the RoutesList options.</param>
        /// <returns>The application builder instance.</returns>
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

            return app;
        }

        /// <summary>
        /// Adds RoutesList middleware to the application pipeline with default configuration.
        /// </summary>
        /// <param name="app">The application builder instance.</param>
        /// <returns>The application builder instance.</returns>
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

            return app;
        }
    }
}
