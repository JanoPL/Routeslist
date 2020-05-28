using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Abstractions;
using System.Linq;
using RoutesList.Models;
using Microsoft.Extensions.Logging;
using ConsoleTables;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using System.Threading.Tasks;

namespace RoutesList
{
    public class RoutesList
    {
        private readonly IActionDescriptorCollectionProvider ActionProvider;
        private readonly ILogger logger;
        private readonly ConsoleTable table = new ConsoleTable("Method", "Uri", "Controller Name", "Action", "Full Name");

        public RoutesList(IActionDescriptorCollectionProvider descriptorCollectionProvider)
        {
            this.ActionProvider = descriptorCollectionProvider;
        }

        public RoutesList(IActionDescriptorCollectionProvider descriptorCollectionProvider, ILogger logger)
        {
            this.ActionProvider = descriptorCollectionProvider;
            this.logger = logger;
        }

        public IEnumerable<ActionDescriptor> ListRoutes()
        {
            IEnumerable<ActionDescriptor> routes = this.ActionProvider.ActionDescriptors.Items.Where(
                routes => routes.AttributeRouteInfo != null
            );

            return routes;
        }

        public List<RoutesInformationModel> GetAllRoutesInformation()
        {
            var routes = ListRoutes();
            List<RoutesInformationModel> routesInformation = new List<RoutesInformationModel>();
            
            foreach (var route in routes)
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

        public void GetLoggerRoutesList()
        {
            
            foreach (var routesInformation in GetAllRoutesInformation())
            {
                this.table.AddRow(routesInformation.Method_name, routesInformation.Template, routesInformation.Controller_name, routesInformation.Action_name, routesInformation.Display_name);
            }

            this.logger.LogDebug(this.table.ToString());
        }

        public async Task<string> GetRoutesList()
        {
            foreach (var routesInformation in GetAllRoutesInformation())
            {
                this.table.AddRow(routesInformation.Method_name, routesInformation.Template, routesInformation.Controller_name, routesInformation.Action_name, routesInformation.Display_name);
            }

            return await Task.FromResult(this.table.ToString());
        }

    }
}
