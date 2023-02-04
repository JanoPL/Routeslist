using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Microsoft.AspNetCore.Mvc.Abstractions;
using RoutesList.Build.Models;
using RoutesList.Build.Services.RoutesBuilder;

namespace RoutesList.Build.Services.Strategies
{
    public class Context
    {
        
        private IStrategy _strategy;
        private readonly ActionDescriptor _actionDescriptor;

        public Context(ActionDescriptor actionDescriptor) 
        {
            _actionDescriptor = actionDescriptor;
        }

        public Context(IStrategy strategy, ActionDescriptor actionDescriptor)
        {
            _strategy = strategy;
            _actionDescriptor = actionDescriptor;
        }

        public void SetStrategy(IStrategy strategy)
        {
            _strategy = strategy;
        }

        public RoutesInformationModel Execute()
        {
            Builder builder = _strategy.Process(_actionDescriptor);

            return builder.Build();
        }
    }
}
