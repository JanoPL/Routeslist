using Microsoft.AspNetCore.Mvc.Controllers;
using RoutesList.Build.Services.Strategies;
using Xunit;

namespace RoutesList.Tests.Services
{
    public partial class RoutesTests
    {
        public class BuildControllerActionDescriptorStrategyTests
        {
            private readonly ControllerActionDescriptor descriptor = new ControllerActionDescriptor();

            // Act
            private bool result;

            private BuildControllerActionDescriptorStrategy strategy = new BuildControllerActionDescriptorStrategy(1);

            [Fact]
            public void CanProcess_WithControllerActionDescriptor_ReturnsTrue()
            {
                // Arrange

                // Act
                result = strategy.CanProcess(descriptor);

                // Assert
                Assert.True(result);
            }
        }
    }
}