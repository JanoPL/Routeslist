using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace RutesList.Gen
{
    public static class RoutesListView
    {
        public static IApplicationBuilder UseRoutesList(
            this IApplicationBuilder app,
            RoutesListOptions options)
        {
            return app.UseMiddleware<RoutesListMiddleware>(options);
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

            return app.UseRoutesList(options);
        }
    }
}
