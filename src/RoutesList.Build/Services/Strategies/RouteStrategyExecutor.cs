using Microsoft.AspNetCore.Mvc.Abstractions;
using RoutesList.Build.Models;

namespace RoutesList.Build.Services.Strategies
{
    /// <summary>
    /// Represents a context for executing route processing strategies.
    /// Implements the strategy pattern to handle different types of route processing.
    /// </summary>
    public class RouteStrategyExecutor
    {
        private readonly ActionDescriptor _actionDescriptor;
        private readonly IRouteProcessingStrategy _strategy;

        /// <summary>
        /// Initializes a new instance of the <see cref="RouteStrategyExecutor"/> class.
        /// </summary>
        /// <param name="strategy">The strategy implementation to process the route.</param>
        /// <param name="actionDescriptor">The action descriptor containing route information.</param>
        public RouteStrategyExecutor(IRouteProcessingStrategy strategy, ActionDescriptor actionDescriptor)
        {
            _strategy = strategy;
            _actionDescriptor = actionDescriptor;
        }

        /// <summary>
        /// Executes the strategy to process the route and build route information.
        /// </summary>
        /// <returns>A <see cref="RoutesInformationModel"/> containing the processed route information.</returns>
        public RoutesInformationModel Execute()
        {
            var builder = _strategy.Process(_actionDescriptor);

            return builder.Build();
        }
    }
}