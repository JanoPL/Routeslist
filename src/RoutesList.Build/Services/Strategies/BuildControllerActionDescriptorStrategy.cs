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

            var controllerName = GetRouteValue(route, "controller");
            var actionName = GetRouteValue(route, "action");
            var displayName = route.DisplayName;
            var template = route.AttributeRouteInfo?.Template;
            var methodName = GetHttpMethod(route);
        
            AddControllerName(controllerName, builder);
            AddActionName(actionName, builder);
            AddDisplayName(displayName, builder);
            AddTemplate(template, builder);
            AddMethodName(methodName, builder);
        
            return builder;
        }
        
        private static string GetRouteValue(ActionDescriptor route, string key)
            => route.RouteValues.First(value => value.Key == key).Value;
        
        private static string GetHttpMethod(ActionDescriptor route)
            => route.ActionConstraints?.OfType<HttpMethodActionConstraint>()
                .SingleOrDefault()?.HttpMethods.First();
    }
}
