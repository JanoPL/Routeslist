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
        private IEnumerable<ActionDescriptor> Items { get; set; }

        public BuildCompiledPageDescriptorStrategy(int id, IEnumerable<ActionDescriptor> items)
        {
            Id = id;
            Items = items;
        }

        public Builder Process(ActionDescriptor route)
        {
            Builder builder = new Builder().Create(Id);

            var compiledPageActionDescriptor = Items.OfType<CompiledPageActionDescriptor>()
                        .Where(r => r.Id == route.Id)
                        .Select(a => new {
                            a.DisplayName,
                            a.ViewEnginePath,
                            a.RelativePath
                        }).First();

            string viewEnginePath = compiledPageActionDescriptor?.ViewEnginePath;
            string relativePath = compiledPageActionDescriptor?.RelativePath;
            string displayName = compiledPageActionDescriptor?.DisplayName;

            builder.IsCompiledPageActionDescriptior(true);

            AddDisplayName(displayName, builder);
            AddViewEnginePath(viewEnginePath, builder);
            AddRelativePath(relativePath, builder);

            return builder;
        }
    }
}
