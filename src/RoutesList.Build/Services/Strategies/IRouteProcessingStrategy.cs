using Microsoft.AspNetCore.Mvc.Abstractions;
using RoutesList.Build.Services.RoutesBuilder;
using System;

namespace RoutesList.Build.Services.Strategies
{
    /// <summary>
    /// Defines a strategy for processing route information in the RoutesList application.
    /// </summary>
    /// <typeparam name="TDescriptor">The type of action descriptor this strategy can process.</typeparam>
    public interface IRouteProcessingStrategy<in TDescriptor> where TDescriptor : ActionDescriptor
    {
        /// <summary>
        /// Checks if this strategy can process the given action descriptor.
        /// </summary>
        /// <param name="descriptor">The action descriptor to check.</param>
        /// <returns>True if this strategy can process the descriptor; otherwise, false.</returns>
        bool CanProcess(ActionDescriptor descriptor);

        /// <summary>
        /// Processes the given action descriptor and builds route information.
        /// </summary>
        /// <param name="descriptor">The action descriptor containing route information to process.</param>
        /// <returns>A Builder instance configured with the processed route information.</returns>
        /// <exception cref="ArgumentException">Thrown when descriptor is not of the expected type.</exception>
        IBuilder Process(TDescriptor descriptor);
    }

    /// <summary>
    /// Simplified non-generic interface that can be used for runtime strategy resolution.
    /// </summary>
    public interface IRouteProcessingStrategy
    {
        /// <summary>
        /// Checks if this strategy can process the given action descriptor.
        /// </summary>
        /// <param name="descriptor">The action descriptor to check.</param>
        /// <returns>True if this strategy can process the descriptor; otherwise, false.</returns>
        bool CanProcess(ActionDescriptor descriptor);

        /// <summary>
        /// Processes the given action descriptor and builds route information.
        /// </summary>
        /// <param name="descriptor">The action descriptor containing route information to process.</param>
        /// <returns>A Builder instance configured with the processed route information.</returns>
        /// <exception cref="ArgumentException">Thrown when the strategy cannot process the descriptor type.</exception>
        IBuilder Process(ActionDescriptor descriptor);
    }
}