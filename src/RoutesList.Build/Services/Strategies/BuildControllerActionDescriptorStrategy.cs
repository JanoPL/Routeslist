using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using RoutesList.Build.Services.RoutesBuilder;

namespace RoutesList.Build.Services.Strategies
{
    #if NET8_0_OR_GREATER
    public class BuildControllerActionDescriptorStrategy(int id) : AbstractBuilderMethod, IStrategy
    {
        private int Id { get; set; } = id;
    #else
    public class BuildControllerActionDescriptorStrategy : AbstractBuilderMethod, IStrategy
    {
        private int Id { get; set; }
    
        public BuildControllerActionDescriptorStrategy(int id)
        {
            Id = id;
        }
    #endif

        public Builder Process(ActionDescriptor route)
        {
            Builder builder = new Builder().Create(Id);
            
            AddControllerName(GetRouteValue(route, "controller"), builder);
            AddActionName(GetRouteValue(route, "action"), builder);
            AddDisplayName(route.DisplayName, builder);
            AddTemplate(route.AttributeRouteInfo?.Template, builder);
            AddMethodName(GetHttpMethod(route), builder);
        
            return builder;
        }
        
        private static string GetRouteValue(ActionDescriptor route, string key)
        {
            // Using TryGetValue for more efficient dictionary lookup
            return route.RouteValues.TryGetValue(key, out var value) ? value : null;
        }
        
        private static string GetHttpMethod(ActionDescriptor route)
        {
            if (route.ActionConstraints == null)
                return null;
                
            // Get HttpMethodActionConstraint in one pass
            var httpConstraint = route.ActionConstraints
                .OfType<HttpMethodActionConstraint>()
                .FirstOrDefault();
                
            // Get the first method if constraint exists and has methods
            return httpConstraint?.HttpMethods?.FirstOrDefault();
        }
    }
}