using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using RoutesList.Interfaces;
using RoutesList.Build.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using RoutesList.Services.RoutesBuilder;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace RoutesList.Services
{
    public class Routes : IRoutes
    {
        public IEnumerable<ActionDescriptor> getRoutes(IActionDescriptorCollectionProvider collectionProvider)
        {
            if (collectionProvider != null) {
                IEnumerable<ActionDescriptor> routes = collectionProvider
                    .ActionDescriptors
                    .Items;

                return routes;
            }

            return Enumerable.Empty<ActionDescriptor>();
        }

        public IList<RoutesInformationModel> getRoutesInformation(IActionDescriptorCollectionProvider collectionProvider)
        {
            IList<RoutesInformationModel> routes = new List<RoutesInformationModel>();

            int id = 1;
            foreach (ActionDescriptor route in getRoutes(collectionProvider)) {

                string actionName = String.Empty;
                string controllerName = String.Empty;
                KeyValuePair<string, string> keyValuePair;

                keyValuePair = route.RouteValues.FirstOrDefault(value => value.Key == "action");

                if (keyValuePair.Key != null) {
                    actionName = keyValuePair.Value;
                }

                keyValuePair = route.RouteValues.FirstOrDefault(value => value.Key == "action");

                if (keyValuePair.Key != null) {
                    controllerName = keyValuePair.Value;
                }

                RoutesInformationModel model = new Builder()
                    .Create(id)
                    .ActionName(route.RouteValues.Where(value => value.Key == "action").First().Value)
                    .DisplayName(route.DisplayName)
                    .ControllerName(route.RouteValues.Where(value => value.Key == "controller").First().Value)
                    .Template(route.AttributeRouteInfo?.Template)
                    .MethodName(route.ActionConstraints?.OfType<HttpMethodActionConstraint>()?.SingleOrDefault()?.HttpMethods?.First<string>())
                    .build();

                routes.Add(model);
            }

            return routes;
        }
    }
}
