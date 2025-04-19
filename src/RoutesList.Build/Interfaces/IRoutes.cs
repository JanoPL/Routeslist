using System.Collections.Generic;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using RoutesList.Build.Models;

namespace RoutesList.Build.Interfaces
{
    public interface IRoutes
    {
        IList<RoutesInformationModel> GetRoutesInformation(IActionDescriptorCollectionProvider collectionProvider);
        public void SetAssembly(Assembly assembly);
    }
}
