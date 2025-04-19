using Microsoft.AspNetCore.Mvc.Abstractions;
using RoutesList.Build.Services.RoutesBuilder;

namespace RoutesList.Build.Services.Strategies
{
    /// <summary>
    /// Defines a strategy interface for processing route information in the RoutesList application.
    /// </summary>
    public interface IStrategy
    {
        /// <summary>
        /// Processes the given route descriptor and builds route information.
        /// </summary>
        /// <param name="route">The action descriptor containing route information to process.</param>
        /// <returns>A Builder instance containing the processed route information.</returns>
        Builder Process(ActionDescriptor route);
    }
}