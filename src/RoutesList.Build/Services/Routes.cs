using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using RoutesList.Build.Interfaces;
using RoutesList.Build.Models;
using RoutesList.Build.Services.Strategies;

namespace RoutesList.Build.Services
{
    public class Routes : IRoutes
    {
        private readonly List<IRouteProcessingStrategy> _strategies;

        public Routes()
        {
            // Initialize with empty list - strategies will be created per request
            _strategies = new List<IRouteProcessingStrategy>();
        }

        private Assembly _assembly { get; set; }

        public void SetAssembly(Assembly assembly)
        {
            _assembly = assembly;
        }

        public IList<RoutesInformationModel> GetRoutesInformation(
            IActionDescriptorCollectionProvider collectionProvider)
        {
            IList<RoutesInformationModel> routes = new List<RoutesInformationModel>();

            var id = 1;
            var items = GetActionDescriptorRoutes(collectionProvider);

            // Initialize strategies once for all routes
            InitializeStrategies(id, items);

            foreach (var route in items)
            {
                // Find the first strategy that can process this route
                var strategy = _strategies.FirstOrDefault(s => s.CanProcess(route));

                if (strategy == null)
                {
                    continue;
                }

                var routeStrategyExecutor = new RouteStrategyExecutor(strategy, route);
                var routesInformationModel = routeStrategyExecutor.Execute();

                routes.Add(routesInformationModel);
                id++;
            }

            IList<RoutesInformationModel> routesInformationModelsItems = GetComponentsRoutes().ToList();
            routes = routes.Union(routesInformationModelsItems).ToList();

            return routes;
        }

        private void InitializeStrategies(int startId, IEnumerable<ActionDescriptor> items)
        {
            _strategies.Clear();

            // Add known strategies
            _strategies.Add(new BuildCompiledPageDescriptorStrategy(startId, items));
            _strategies.Add(new BuildControllerActionDescriptorStrategy(startId));

            // Could add more strategies here in the future
        }

        private IEnumerable<ActionDescriptor> GetActionDescriptorRoutes(
            IActionDescriptorCollectionProvider collectionProvider)
        {
            if (collectionProvider == null)
            {
                return Enumerable.Empty<ActionDescriptor>();
            }
            
            IEnumerable<ActionDescriptor> routes = collectionProvider
                .ActionDescriptors
                .Items;

            return routes;

        }

        private IEnumerable<RoutesInformationModel> GetComponentsRoutes()
        {
            var componentsRoutes = RoutesComponent.GetRoutesToRender(_assembly);
            var routesInformationModels = componentsRoutes.ToList();

            return routesInformationModels.Any() ? routesInformationModels : Enumerable.Empty<RoutesInformationModel>();
        }
    }
}