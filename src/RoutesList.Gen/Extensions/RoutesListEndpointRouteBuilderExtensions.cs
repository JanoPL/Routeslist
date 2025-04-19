using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace RoutesList.Gen.Extensions
{
    public static class RoutesListEndpointRouteBuilderExtensions
    {
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