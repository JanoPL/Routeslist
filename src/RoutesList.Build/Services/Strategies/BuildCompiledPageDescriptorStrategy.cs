using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RoutesList.Build.Services.RoutesBuilder;

namespace RoutesList.Build.Services.Strategies
{
    public class BuildCompiledPageDescriptorStrategy : AbstractBuilderMethod, IStrategy
    {
        private int Id { set; get; }
        private readonly Dictionary<string, CompiledPageActionDescriptor> _compiledPageLookup;

        public BuildCompiledPageDescriptorStrategy(int id, IEnumerable<ActionDescriptor> items)
        {
            Id = id;
            // Pre-process the items once during initialization
            _compiledPageLookup = items.OfType<CompiledPageActionDescriptor>()
                .ToDictionary(a => a.Id.ToString());
        }

        public Builder Process(ActionDescriptor route)
        {
            Builder builder = new Builder().Create(Id);

            if (_compiledPageLookup.TryGetValue(route.Id, out CompiledPageActionDescriptor descriptor))
            {
                builder.IsCompiledPageActionDescriptior(true);

                AddDisplayName(descriptor.DisplayName, builder);
                AddViewEnginePath(descriptor.ViewEnginePath, builder);
                AddRelativePath(descriptor.RelativePath, builder);
            }

            return builder;
        }
    }
}