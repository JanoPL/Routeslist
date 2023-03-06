using System.Collections.Generic;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using RoutesList.Build.Models;

namespace RoutesList.Interfaces
{
    public interface IRoutes
    {
        IList<RoutesInformationModel> getRoutesInformation(IActionDescriptorCollectionProvider collectionProvider);
        public void SetAssembly(Assembly assembly);
    }
}
