using System.Collections.Generic;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using RoutesList.Build.Models;

namespace RoutesList.Build.Interfaces
{
    /// <summary>
    /// Defines methods for retrieving and managing route information.
    /// </summary>
    public interface IRoutes
    {
        /// <summary>
        /// Retrieves route information from the provided action descriptor collection.
        /// </summary>
        /// <param name="collectionProvider">The provider containing action descriptors for route analysis.</param>
        /// <returns>A list of route information models representing the application's routes.</returns>
        IList<RoutesInformationModel> GetRoutesInformation(IActionDescriptorCollectionProvider collectionProvider);
    
        /// <summary>
        /// Sets the assembly to be used for route analysis.
        /// </summary>
        /// <param name="assembly">The assembly containing the routes to analyze.</param>
        public void SetAssembly(Assembly assembly);
    }
}
