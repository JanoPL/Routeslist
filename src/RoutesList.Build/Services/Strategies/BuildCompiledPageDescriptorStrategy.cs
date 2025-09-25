using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RoutesList.Build.Extensions;
using RoutesList.Build.Services.RoutesBuilder;

namespace RoutesList.Build.Services.Strategies
{
    /// <summary>
    /// Strategy for processing and building compiled page descriptors in the routing system.
    /// Implements the <see cref="IRouteProcessingStrategy"/> interface to handle CompiledPageActionDescriptor routes.
    /// </summary>
    public class BuildCompiledPageDescriptorStrategy : IRouteProcessingStrategy
    {
        /// <summary>
        /// Gets or sets the identifier for this strategy instance.
        /// </summary>
        private int Id { set; get; }
        
        /// <summary>
        /// Dictionary storing compiled page descriptors indexed by their ID.
        /// </summary>
        private readonly Dictionary<string, CompiledPageActionDescriptor> _compiledPageLookup;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="BuildCompiledPageDescriptorStrategy"/> class.
        /// </summary>
        /// <param name="id">The identifier for this strategy instance.</param>
        /// <param name="items">Collection of action descriptors to process.</param>
        public BuildCompiledPageDescriptorStrategy(int id, IEnumerable<ActionDescriptor> items)
        {
            Id = id;
            // Pre-process the items once during initialization
            _compiledPageLookup = items.OfType<CompiledPageActionDescriptor>()
                .ToDictionary(a => a.Id.ToString());
        }

        /// <summary>
        /// Determines whether this strategy can process the specified action descriptor.
        /// </summary>
        /// <param name="descriptor">The action descriptor to check.</param>
        /// <returns>true if the descriptor is a CompiledPageActionDescriptor; otherwise, false.</returns>
        public bool CanProcess(ActionDescriptor descriptor)
        {
            return descriptor is CompiledPageActionDescriptor;
        }

        /// <summary>
        /// Processes the specified route and builds a route descriptor.
        /// </summary>
        /// <param name="route">The route to process.</param>
        /// <returns>An <see cref="IBuilder"/> instance containing the processed route information.</returns>
        public IBuilder Process(ActionDescriptor route)
        {
            IBuilder builder = new Builder().Create(Id);
            
            if (_compiledPageLookup.TryGetValue(route.Id, out CompiledPageActionDescriptor descriptor))
            {
                builder.IsCompiledPageActionDescriptior(true)
                    .SafeDisplayName(descriptor.DisplayName)
                    .SafeViewEnginePath(descriptor.ViewEnginePath)
                    .SafeRelativePath(descriptor.RelativePath);
            }

            return builder;
        }
    }
}