using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Text;

namespace RoutesList.Gen.Extensions
{
    public static class RoutesListEndpointRouteBuilderExtensions
    {
        public static IEndpointConventionBuilder MapRouteList(
            this IEndpointRouteBuilder endpoints,
            string pattern,
            RoutesListOptions options
        ) {
            var pipeline = endpoints.CreateApplicationBuilder()
                .UseMiddleware<RoutesListMiddleware>(options)
                .Build();

            return endpoints.Map(pattern, pipeline).WithDisplayName("Routes List");
        }
    }
}
