using System.Linq;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.AspNetCore.Mvc.Controllers;
using RoutesList.Build.Extensions;
using RoutesList.Build.Services.RoutesBuilder;

namespace RoutesList.Build.Services.Strategies
{
    #if NET8_0_OR_GREATER
    /// <summary>
    /// Strategy for processing controller action descriptors and building route information.
    /// </summary>
    /// <param name="id">The identifier used for route creation.</param>
    public class BuildControllerActionDescriptorStrategy(int id) : RouteProcessingStrategyBase<ControllerActionDescriptor>(id)
    {
        private new int Id { get; set; } = id;
    #else
    /// <summary>
    /// Represents a strategy to process and build data from <see cref="ControllerActionDescriptor"/> objects.
    /// </summary>
    /// <remarks>
    /// This strategy is specifically designed to handle descriptors of type <see cref="ControllerActionDescriptor"/>.
    /// It extracts relevant route information and constructs a builder that encapsulates route and controller details.
    /// </remarks>
    public class BuildControllerActionDescriptorStrategy : RouteProcessingStrategyBase<ControllerActionDescriptor>

    {
        private new int Id { get; set; }
        /// <summary>
        /// A strategy for processing controller action descriptors and building specific route information.
        /// </summary>
        public BuildControllerActionDescriptorStrategy(int id) : base(id)
        {
        }
#endif

        /// <summary>
        /// Determines whether this strategy can process the specified action descriptor.
        /// </summary>
        /// <param name="descriptor">The action descriptor to check.</param>
        /// <returns>True if the descriptor is a ControllerActionDescriptor; otherwise, false.</returns>
        public new bool CanProcess(ActionDescriptor descriptor)
        {
            return descriptor is ControllerActionDescriptor;
        }

        /// <summary>
        /// Processes the controller action descriptor and builds route information.
        /// </summary>
        /// <param name="descriptor">The controller action descriptor to process.</param>
        /// <returns>A builder instance containing the processed route information.</returns>
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
        
        /// <summary>
        /// Retrieves a route value from the action descriptor's route values dictionary.
        /// </summary>
        /// <param name="route">The action descriptor containing route values.</param>
        /// <param name="key">The key to look up in the route values.</param>
        /// <returns>The route value if found; otherwise, null.</returns>
        private static string GetRouteValue(ActionDescriptor route, string key)
        {
            // Using TryGetValue for a more efficient dictionary lookup
            return route.RouteValues.TryGetValue(key, out var value) ? value : null;
        }
        
        /// <summary>
        /// Extracts the HTTP method from the action descriptor's constraints.
        /// </summary>
        /// <param name="route">The action descriptor containing HTTP method constraints.</param>
        /// <returns>The first HTTP method if found; otherwise, null.</returns>
        private static string GetHttpMethod(ActionDescriptor route)
        {
            if (route.ActionConstraints == null)
            {
                return null;
            }

            // Get HttpMethodActionConstraint in one pass
            var httpConstraint = route.ActionConstraints
                .OfType<HttpMethodActionConstraint>()
                .FirstOrDefault();
                
            // Get the first method if the constraint exists and has methods
            return httpConstraint?.HttpMethods.FirstOrDefault();
        }
    }
}