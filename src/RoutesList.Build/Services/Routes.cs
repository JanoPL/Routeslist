using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using RoutesList.Build.Models;
using RoutesList.Build.Services.Strategies;
using RoutesList.Interfaces;

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
            IEnumerable<ActionDescriptor> items = getRoutes(collectionProvider);

            foreach (ActionDescriptor route in items) {

                RoutesInformationModel routesInformationModel;
                if (IsCompiledPageDescriptor(route)) {
                    var context = new Context(new BuildCompiledPageDescriptorStrategy(id, items), route);

                    routesInformationModel = context.Execute();
                } else if (IsControllerActionDescriptor(route)) {
                    var context = new Context(new BuildControllerActionDescriptorStrategy(id), route);

                    routesInformationModel = context.Execute();
                } else {
                    continue;
                }

                routes.Add(routesInformationModel);
                id++;
            }

            return routes;
        }

        private static bool IsCompiledPageDescriptor(ActionDescriptor actionDescriptor)
        {
            return actionDescriptor.GetType().FullName == "Microsoft.AspNetCore.Mvc.RazorPages.CompiledPageActionDescriptor";
        }

        private static bool IsControllerActionDescriptor(ActionDescriptor actionDescriptor)
        {
            return actionDescriptor.GetType().FullName == "Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor";
        }
    }
}
