using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using RoutesList.Build.Models;

namespace RoutesList.Interfaces
{
    public interface IRoutes
    {
        IEnumerable<ActionDescriptor> getRoutes(IActionDescriptorCollectionProvider collectionProvider);
        IList<RoutesInformationModel> getRoutesInformation(IActionDescriptorCollectionProvider collectionProvider);
    }
}
