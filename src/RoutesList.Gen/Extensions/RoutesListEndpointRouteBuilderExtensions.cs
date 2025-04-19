using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using RoutesList.Build.Models;

namespace RoutesList.Gen.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="IEndpointRouteBuilder"/> to add routes list functionality.
    /// </summary>
    public static class RoutesListEndpointRouteBuilderExtensions
    {
        /// <summary>
        /// Maps the routes list middleware to the specified pattern with given options.
        /// </summary>
        /// <param name="endpoints">The <see cref="IEndpointRouteBuilder"/> to add the route to.</param>
        /// <param name="pattern">The route pattern to map the routes list to.</param>
        /// <param name="options">The options to configure the routes list behavior.</param>
        /// <returns>A <see cref="IEndpointConventionBuilder"/> that can be used to further customize the endpoint.</returns>
        public static IEndpointConventionBuilder MapRouteList(
            this IEndpointRouteBuilder endpoints,
            string pattern,
            RoutesListOptions options
        )
        {
            var pipeline = endpoints.CreateApplicationBuilder()
                .UseMiddleware<RoutesListMiddleware>(options)
                .Build();

            return endpoints.Map(pattern, pipeline);
        }
    }
}