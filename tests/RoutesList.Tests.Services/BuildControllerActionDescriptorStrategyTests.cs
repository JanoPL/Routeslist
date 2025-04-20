using Microsoft.AspNetCore.Mvc.Controllers;
using RoutesList.Build.Services.Strategies;
using Xunit;

namespace RoutesList.Tests.Services
{
    public class BuildControllerActionDescriptorStrategyTests
    {
        private readonly ControllerActionDescriptor _descriptor = new ControllerActionDescriptor();

        // Act
        private bool _result;

        private readonly BuildControllerActionDescriptorStrategy _strategy = new BuildControllerActionDescriptorStrategy(1);

        [Fact]
        public void CanProcess_WithControllerActionDescriptor_ReturnsTrue()
        {
            // Arrange

            // Act
            _result = _strategy.CanProcess(_descriptor);

            // Assert
            Assert.True(_result);
        }
    }
}