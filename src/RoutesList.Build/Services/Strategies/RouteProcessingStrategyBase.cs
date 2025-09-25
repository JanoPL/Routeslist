using System;
using Microsoft.AspNetCore.Mvc.Abstractions;
using RoutesList.Build.Services.RoutesBuilder;

namespace RoutesList.Build.Services.Strategies
{
    /// <summary>
    /// Base implementation for route processing strategies that handle specific ActionDescriptor types.
    /// Provides a common foundation for processing different types of routes in the application.
    /// </summary>
    /// <typeparam name="TDescriptor">The specific ActionDescriptor type this strategy processes.</typeparam>
    /// <remarks>
    /// This abstract class implements both the generic and non-generic versions of IRouteProcessingStrategy
    /// to provide type-safe route processing while maintaining compatibility with non-generic contexts.
    /// </remarks>
    public abstract class RouteProcessingStrategyBase<TDescriptor> : 
        IRouteProcessingStrategy<TDescriptor>, 
        IRouteProcessingStrategy 
        where TDescriptor : ActionDescriptor
    {
        /// <summary>
        /// Gets the unique identifier for this route processing strategy.
        /// </summary>
        /// <value>
        /// The integer identifier assigned to this strategy instance.
        /// </value>
        protected int Id { get; }


        /// <summary>
        /// Initializes a new instance of the RouteProcessingStrategyBase class.
        /// </summary>
        /// <param name="id">The unique identifier for this strategy instance.</param>
        protected RouteProcessingStrategyBase(int id)
        {
            Id = id;
        }

        /// <summary>
        /// Determines whether this strategy can process the given action descriptor.
        /// </summary>
        /// <param name="descriptor">The action descriptor to check.</param>
        /// <returns>True if the descriptor is of type TDescriptor; otherwise, false.</returns>
        public bool CanProcess(ActionDescriptor descriptor)
        {
            return descriptor is TDescriptor;
        }

        /// <inheritdoc/>
        public abstract IBuilder Process(TDescriptor descriptor);

        /// <summary>
        /// Processes the given action descriptor and builds route information.
        /// </summary>
        /// <param name="descriptor">The action descriptor to process.</param>
        /// <returns>A Builder instance containing the processed route information.</returns>
        /// <exception cref="ArgumentException">Thrown when the descriptor cannot be processed by this strategy.</exception>
        /// <inheritdoc/>
        IBuilder IRouteProcessingStrategy.Process(ActionDescriptor descriptor)
        {
            if (descriptor is TDescriptor typedDescriptor)
            {
                return Process(typedDescriptor);
            }
        
            throw new ArgumentException($"Cannot process descriptor of type {descriptor.GetType().Name}", nameof(descriptor));
        }
    }
}