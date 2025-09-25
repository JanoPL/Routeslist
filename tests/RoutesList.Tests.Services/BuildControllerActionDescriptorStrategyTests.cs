using Microsoft.AspNetCore.Mvc.Controllers;
using RoutesList.Build.Services.Strategies;
using Xunit;

namespace RoutesList.Tests.Services
{
    public class BuildControllerActionDescriptorStrategyTests
    {
        private readonly ControllerActionDescriptor _descriptor = new ControllerActionDescriptor();

        private readonly BuildControllerActionDescriptorStrategy _strategy = new BuildControllerActionDescriptorStrategy(1);

        [Fact]
        public void CanProcess_WithControllerActionDescriptor_ReturnsTrue()
        {
            bool result = _strategy.CanProcess(_descriptor);

            Assert.True(result);
        }
    }
}