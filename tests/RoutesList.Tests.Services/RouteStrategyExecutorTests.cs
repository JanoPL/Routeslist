using Microsoft.AspNetCore.Mvc.Abstractions;
using Moq;
using RoutesList.Build.Models;
using RoutesList.Build.Services.RoutesBuilder;
using RoutesList.Build.Services.Strategies;
using Xunit;

namespace RoutesList.Tests.Services
{
    public class RouteStrategyExecutorTests
    {
        [Fact]
        public void Execute_CallsStrategyProcess_AndReturnsBuildResult()
        {
            // Arrange
            var Id = 42;
            var mockStrategy = new Mock<IRouteProcessingStrategy>();
            var mockBuilder = new Mock<IBuilder>();
            var mockRoute = new ActionDescriptor();
            string controllerName = "Test";
            string actionName = "Action";
            string displayName = $"{controllerName}.Controller.{actionName}";
            bool isCompiledPageActionDescriptor = false;
            var mockResult = new RoutesInformationModel { Id = Id, IsCompiledPageActionDescriptor = isCompiledPageActionDescriptor, ControllerName = controllerName, ActionName = actionName, DisplayName = displayName};

            mockBuilder.Setup(b => b.Build()).Returns(mockResult);
            mockStrategy.Setup(s => s.Process(mockRoute)).Returns(mockBuilder.Object);
            
            var executor = new RouteStrategyExecutor(mockStrategy.Object, mockRoute);

            // Act
            var result = executor.Execute();

            // Assert
            Assert.Same(mockResult, result);
            mockStrategy.Verify(s => s.Process(mockRoute), Times.Once);
            mockBuilder.Verify(b => b.Build(), Times.Once);
        }
    }
}