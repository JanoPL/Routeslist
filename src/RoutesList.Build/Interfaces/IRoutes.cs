using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using RoutesList.Build.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RoutesList.Interfaces
{
    public interface IRoutes
    {
        IEnumerable<ActionDescriptor> getRoutes(IActionDescriptorCollectionProvider collectionProvider);
        IList<RoutesInformationModel> getRoutesInformation(IActionDescriptorCollectionProvider collectionProvider);
    }
}
