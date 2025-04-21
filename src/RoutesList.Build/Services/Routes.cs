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
    /// <summary>
    /// The Routes class is responsible for managing and retrieving information
    /// about application routes. It implements the <see cref="IRoutes"/> interface.
    /// </summary>
    public class Routes : IRoutes
    {
        private readonly List<IRouteProcessingStrategy> _strategies;

        /// <summary>
        /// Initializes a new instance of the Routes class.
        /// The constructor initializes an empty list of route processing strategies
        /// that will be populated per request.
        /// </summary>
        public Routes()
        {
            // Initialize with empty list - strategies will be created per request
            _strategies = new List<IRouteProcessingStrategy>();
        }

        private Assembly _assembly { get; set; }

        /// <summary>
        /// Sets the assembly to be used for route processing.
        /// </summary>
        /// <param name="assembly">The assembly to be processed for routes.</param>
        public void SetAssembly(Assembly assembly)
        {
            _assembly = assembly;
        }

        /// <summary>
        /// Retrieves route information from the provided collection provider.
        /// </summary>
        /// <param name="collectionProvider">The action descriptor collection provider containing route information.</param>
        /// <returns>A list of route information models containing processed route data.</returns>
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

        /// <summary>
        /// Initializes the route processing strategies with the given start ID and action descriptors.
        /// </summary>
        /// <param name="startId">The starting ID for route numbering.</param>
        /// <param name="items">The collection of action descriptors to process.</param>
        private void InitializeStrategies(int startId, IEnumerable<ActionDescriptor> items)
        {
            _strategies.Clear();

            // Add known strategies
            _strategies.Add(new BuildCompiledPageDescriptorStrategy(startId, items));
            _strategies.Add(new BuildControllerActionDescriptorStrategy(startId));

            // Could add more strategies here in the future
        }

        /// <summary>
        /// Retrieves action descriptors from the provided collection provider.
        /// </summary>
        /// <param name="collectionProvider">The action descriptor collection provider.</param>
        /// <returns>A collection of action descriptors, or an empty enumerable if the provider is null.</returns>
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

        /// <summary>
        /// Retrieves routes information from components in the current assembly.
        /// </summary>
        /// <returns>A collection of routes information models from components, or an empty enumerable if none are found.</returns>
        private IEnumerable<RoutesInformationModel> GetComponentsRoutes()
        {
            var componentsRoutes = RoutesComponent.GetRoutesToRender(_assembly);
            var routesInformationModels = componentsRoutes.ToList();

            return routesInformationModels.Any() ? routesInformationModels : Enumerable.Empty<RoutesInformationModel>();
        }
    }
}