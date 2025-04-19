using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Components;
using RoutesList.Build.Models;

namespace RoutesList.Build.Services
{
    public static class RoutesComponent
    {
#if NETCOREAPP3_1
        
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
        public static IEnumerable<RoutesInformationModel> GetRoutesToRender(Assembly assembly)
        {
            if (assembly == null) {
                return Enumerable.Empty<RoutesInformationModel>();
            }

            var components = assembly
                .ExportedTypes
                .Where(t => t.IsSubclassOf(typeof(ComponentBase)));

            var list = components
                .Select(component => GetRouteFromComponent(component))
                .Where(config => config is not null);

            return list;
        }
#endif

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
                throw new Exception($"RouteAttribute in component '{component}' has empty route template");
            }

            // Doesn't support tokens yet
            if (route.Template.Contains('{')) {
                throw new Exception($"RouteAttribute for component '{component}' contains route values. Route values are invalid for prerendering");
            }

            return route;
        }
    }
}
