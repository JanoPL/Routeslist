using System.Linq;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.Controllers;
using RoutesList.Build.Extensions;
using RoutesList.Build.Services.RoutesBuilder;

namespace RoutesList.Build.Services.Strategies
{
    #if NET8_0_OR_GREATER
    public class BuildControllerActionDescriptorStrategy(int id) : RouteProcessingStrategyBase<ControllerActionDescriptor>(id)
    {
        private new int Id { get; set; } = id;
    #else
    public class BuildControllerActionDescriptorStrategy : RouteProcessingStrategyBase<ControllerActionDescriptor>

    {
        private new int Id { get; set; }
        public BuildControllerActionDescriptorStrategy(int id) : base(id) { }
    #endif

        public new bool CanProcess(ActionDescriptor descriptor)
        {
            return descriptor is ControllerActionDescriptor;
        }

        public override IBuilder Process(ControllerActionDescriptor descriptor)
        {
            return new Builder()
                .Create(Id)
                .SafeControllerName(GetRouteValue(descriptor, "controller"))
                .SafeActionName(GetRouteValue(descriptor, "action"))
                .SafeDisplayName(descriptor.DisplayName)
                .SafeTemplate(descriptor.AttributeRouteInfo?.Template)
                .SafeMethodName(GetHttpMethod(descriptor))
            ;
        }
        
        private static string GetRouteValue(ActionDescriptor route, string key)
        {
            // Using TryGetValue for a more efficient dictionary lookup
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
                
            // Get the first method if the constraint exists and has methods
            return httpConstraint?.HttpMethods.FirstOrDefault();
        }
    }
}