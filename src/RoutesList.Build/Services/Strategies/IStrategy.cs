using Microsoft.AspNetCore.Mvc.Abstractions;
using RoutesList.Build.Services.RoutesBuilder;

namespace RoutesList.Build.Services.Strategies
{
    public interface IStrategy
    {
        Builder Process(ActionDescriptor route);
    }
}