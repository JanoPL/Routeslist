using System.Linq;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using RoutesList.Build.Services.RoutesBuilder;

namespace RoutesList.Build.Services.Strategies
{
    public class BuildControllerActionDescriptorStrategy : AbstractBuilderMethod, IStrategy
    {
        private int Id { get; set; }

        public BuildControllerActionDescriptorStrategy(int id)
        {
            Id = id;
        }

        public Builder Process(ActionDescriptor route)
        {
            Builder builder = new Builder().Create(Id);

            string controllerName = route.RouteValues.Where(value => value.Key == "controller").First().Value;
            string actionName = route.RouteValues.Where(value => value.Key == "action").First().Value;
            string displayName = route.DisplayName;
            string template = route.AttributeRouteInfo?.Template;
            string methodName = route.ActionConstraints?.OfType<HttpMethodActionConstraint>()?.SingleOrDefault()?.HttpMethods?.First<string>();

            AddControllerName(controllerName, builder);
            AddActionName(actionName, builder);
            AddDisplayName(displayName, builder);
            AddTemplate(template, builder);
            AddMethodName(methodName, builder);

            return builder;
        }
    }
}
