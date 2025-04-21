using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Components;
using RoutesList.Build.Models;

namespace RoutesList.Build.Services
{
    /// <summary>
    ///     Provides functionality for retrieving and processing Blazor component routes.
    ///     This class scans assemblies for Blazor components with route attributes and
    ///     converts them into structured route information models for further processing.
    /// </summary>
    public static class RoutesComponent
    {
        /// <summary>
        ///     Retrieves all routable components from the specified assembly and converts them to route information models.
        ///     Only components decorated with RouteAttribute are processed and included in the result.
        /// </summary>
        /// <param name="assembly">The assembly to scan for routable components. If null, returns an empty collection.</param>
        /// <returns>A collection of route information models representing the routable components. Returns an empty collection if no components are found or the assembly is null.</returns>
        public static IEnumerable<RoutesInformationModel> GetRoutesToRender(Assembly assembly)
        {
            if (assembly == null)
            {
                return Enumerable.Empty<RoutesInformationModel>();
            }

            var components = assembly
                .ExportedTypes
                .Where(t => t.IsSubclassOf(typeof(ComponentBase)));

            return components
                .Select(GetRouteFromComponent)
                .Where(config => config != null);
        }

        /// <summary>
        ///     Extracts route information from a component type and creates a corresponding route information model.
        ///     Processes RouteAttribute to determine the component's routing configuration.
        /// </summary>
        /// <param name="component">The component type to extract route information from. Must be a Blazor component type.</param>
        /// <returns>A route information model containing the component's routing details if the component has a RouteAttribute; otherwise, null.</returns>
        /// <exception cref="ArgumentException">Thrown when the route template is empty or contains route parameter tokens ('{' character).</exception>
        /// <remarks>
        ///     The method creates a RoutesInformationModel with the following properties:
        ///     - Template: The route template from RouteAttribute
        ///     - DisplayName: The full name of the component
        ///     - RelativePath: The component's path derived from its namespace
        ///     - IsCompiledPageActionDescriptor: Always set to true
        /// </remarks>
        private static RoutesInformationModel GetRouteFromComponent(Type component)
        {
            var attributes = component.GetCustomAttributes(true);

            var routeAttribute = attributes.OfType<RouteAttribute>().FirstOrDefault();

            if (routeAttribute is null)
            {
                // Only map routable components
                return null;
            }

            var route = new RoutesInformationModel
            {
                Template = routeAttribute.Template,
                DisplayName = component.FullName,
                RelativePath = component.FullName?.Replace(".", "/") + ".razor",
                IsCompiledPageActionDescriptor = true
            };

            if (string.IsNullOrEmpty(route.Template))
            {
                throw new ArgumentException($"RouteAttribute in component '{component}' has empty route template");
            }

            // Doesn't support tokens yet
            if (route.Template.Contains('{'))
            {
                throw new ArgumentException(
                    $"RouteAttribute for component '{component}' contains route values. Route values are invalid for prerendering");
            }

            return route;
        }
    }
}