using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using RoutesList.Build.Interfaces;
using RoutesList.Build.Models;
using RoutesList.Build.Services;
using RoutesList.Build.Services.Strategies;

namespace RoutesList.Services
{
    public class Routes : IRoutes
    {
        private Assembly _assembly { get; set; }

        public void SetAssembly(Assembly assembly)
        {
            _assembly = assembly;
        }

        public IList<RoutesInformationModel> GetRoutesInformation(IActionDescriptorCollectionProvider collectionProvider)
        {
            IList<RoutesInformationModel> routes = new List<RoutesInformationModel>();

            int id = 1;
            IEnumerable<ActionDescriptor> items = GetActionDescriptorRoutes(collectionProvider);

            foreach (ActionDescriptor route in items) {
                RoutesInformationModel routesInformationModel;
                Context context;

                if (IsCompiledPageDescriptor(route)) {
                    context = new Context(new BuildCompiledPageDescriptorStrategy(id, items), route);
                } else if (IsControllerActionDescriptor(route)) {
                    context = new Context(new BuildControllerActionDescriptorStrategy(id), route);
                } else {
                    continue;
                }

                routesInformationModel = context.Execute();

                routes.Add(routesInformationModel);
                id++;
            }

            IList<RoutesInformationModel> routesInformationModelsItems = GetComponentsRoutes().ToList();
            routes = routes.Union(routesInformationModelsItems).ToList();
            
            return routes;
        }

        private IEnumerable<ActionDescriptor> GetActionDescriptorRoutes(IActionDescriptorCollectionProvider collectionProvider)
        {
            if (collectionProvider != null) {
                IEnumerable<ActionDescriptor> routes = collectionProvider
                    .ActionDescriptors
                    .Items;

                return routes;
            }

            return Enumerable.Empty<ActionDescriptor>();
        }

        private IEnumerable<RoutesInformationModel> GetComponentsRoutes()
        {
            IEnumerable<RoutesInformationModel> componentsRoutes = RoutesComponent.GetRoutesToRender(_assembly);
            List<RoutesInformationModel> routesInformationModels = componentsRoutes.ToList();
            
            return routesInformationModels.Any() ? routesInformationModels : Enumerable.Empty<RoutesInformationModel>();
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
