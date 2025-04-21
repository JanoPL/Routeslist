using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Components;
using RoutesList.Build.Models;

namespace RoutesList.Build.Services
{
    /// <summary>
    /// Provides functionality for retrieving and processing Blazor component routes.
    /// </summary>
    public static class RoutesComponent
    {
#if NETCOREAPP3_1
        
        /// <summary>
        /// Retrieves all routable components from the specified assembly and converts them to route information models.
        /// </summary>
        /// <param name="assembly">The assembly to scan for routable components.</param>
        /// <returns>A collection of route information models representing the routable components.</returns>
        public static IEnumerable<RoutesInformationModel> GetRoutesToRender(Assembly assembly)
        {
            if (assembly == null) {
                return Enumerable.Empty<RoutesInformationModel>();
            }

            var components = assembly
                .ExportedTypes
                .Where(t => t.IsSubclassOf(typeof(ComponentBase)));

            return components
                .Select(components => GetRouteFromComponent(components))
                .Where(config => config != null);
        }
#endif

#if NET5_0_OR_GREATER
        /// <summary>
        /// Retrieves a collection of route information models for Blazor components within the specified assembly.
        /// </summary>
        /// <param name="assembly">The assembly to inspect for routable Blazor components.</param>
        /// <returns>A collection of <see cref="RoutesInformationModel"/> representing the routes found in the assembly, or an empty collection if no routes are found or the assembly is null.</returns>
        public static IEnumerable<RoutesInformationModel> GetRoutesToRender(Assembly assembly)
        {
            if (assembly == null) {
                return Enumerable.Empty<RoutesInformationModel>();
            }

            var components = assembly
                .ExportedTypes
                .Where(t => t.IsSubclassOf(typeof(ComponentBase)));

            var list = components
                .Select(GetRouteFromComponent)
                .Where(config => config is not null);

            return list;
        }
#endif

        /// <summary>
        /// Extracts route information from a component type.
        /// </summary>
        /// <param name="component">The component type to extract route information from.</param>
        /// <returns>A route information model if the component is routable; otherwise, null.</returns>
        /// <exception cref="ArgumentException">Thrown when route template is empty or contains route values.</exception>
        private static RoutesInformationModel GetRouteFromComponent(Type component)
        {
            var attributes = component.GetCustomAttributes(inherit: true);

            var routeAttribute = attributes.OfType<RouteAttribute>().FirstOrDefault();

            if (routeAttribute is null) {
                // Only map routable components
                return null;
            }

            RoutesInformationModel route = new RoutesInformationModel();

            route.Template = routeAttribute.Template;
            route.DisplayName = component.FullName;

            route.RelativePath = component.FullName.Replace(".", "/") + ".razor";

            route.IsCompiledPageActionDescriptor = true;

            if (string.IsNullOrEmpty(route.Template)) {
                throw new ArgumentException($"RouteAttribute in component '{component}' has empty route template");
            }

            // Doesn't support tokens yet
            if (route.Template.Contains('{')) {
                throw new ArgumentException($"RouteAttribute for component '{component}' contains route values. Route values are invalid for prerendering");
            }

            return route;
        }
    }
}
