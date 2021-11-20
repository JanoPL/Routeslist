using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Abstractions;
using System.Linq;
using RoutesList.Models;
using ConsoleTables;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using System.Threading.Tasks;

namespace RoutesList
{
    /// <summary>
    /// Route List Class
    /// </summary>
    public static class RoutesList
    {
        private static readonly ConsoleTable table = new ConsoleTable("Method", "Uri", "Controller Name", "Action", "Full Name");

        private static IEnumerable<ActionDescriptor> ListRoutes(IActionDescriptorCollectionProvider collectionProvider)
        {
            IEnumerable<ActionDescriptor> routes = collectionProvider.ActionDescriptors.Items.Where(
                routes => routes.AttributeRouteInfo != null
            );

            return routes;
        }

        private static List<RoutesInformationModel> GetAllRoutesInformation(IActionDescriptorCollectionProvider collectionProvider)
        {
            List<RoutesInformationModel> routesInformation = new List<RoutesInformationModel>();
            
            foreach (var route in ListRoutes(collectionProvider))
            {
                string controller_name = String.Empty;
                string action_name = String.Empty;

                foreach (var routeValue in route.RouteValues)
                {
                    if (String.IsNullOrEmpty(controller_name) && routeValue.Key == "controller")
                    {
                        controller_name = routeValue.Value;
                    }

                    if (String.IsNullOrEmpty(action_name) && routeValue.Key == "action") {
                        action_name = routeValue.Value;
                    }

                }

                routesInformation.Add(new RoutesInformationModel()
                {
                    Controller_name = controller_name,
                    Display_name = route.DisplayName,
                    Template = route.AttributeRouteInfo.Template,
                    Action_name = action_name,
                    Method_name = route.ActionConstraints?.OfType<HttpMethodActionConstraint>()?.SingleOrDefault()?.HttpMethods?.First<string>()
            });
                
            }

            return routesInformation;
        }

        /// <summary>
        /// Generate Table as String
        /// </summary>
        /// <param name="collectionProvider"></param>
        /// <returns></returns>
        [Obsolete]
        public static async Task<string> AsyncGetRoutesList(IActionDescriptorCollectionProvider collectionProvider)
        {
            table.Rows.Clear();

            foreach (var routesInformation in GetAllRoutesInformation(collectionProvider))
            {
                table.AddRow(routesInformation.Method_name, routesInformation.Template, routesInformation.Controller_name, routesInformation.Action_name, routesInformation.Display_name);
            }

            return await Task.FromResult(table.ToString());
        }
    }
}
