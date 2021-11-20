using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using RoutesList.Interfaces;
using RoutesList.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoutesList.Services.RoutesBuilder;

namespace RoutesList.Services
{
    public class Routes : IRoutes
    {
        private IList<RoutesInformationModel> _routes = new List<RoutesInformationModel>();

        public IEnumerable<ActionDescriptor> getRoutes(IActionDescriptorCollectionProvider collectionProvider)
        {
            if (collectionProvider != null) {
                IEnumerable<ActionDescriptor> routes = collectionProvider
                    .ActionDescriptors
                    .Items
                    .Where(routes => routes.AttributeRouteInfo != null);

                return routes;
            }

            return Enumerable.Empty<ActionDescriptor>();
        }

        public IList<RoutesInformationModel> getRoutesInformation(IActionDescriptorCollectionProvider collectionProvider)
        {
            int id = 1;
            foreach (ActionDescriptor route in getRoutes(collectionProvider)) {
                RoutesInformationModel model = new Builder()
                    .Create(id)
                    .build();

                _routes.Add(model);
            }

            return _routes;
        }
    }
}
