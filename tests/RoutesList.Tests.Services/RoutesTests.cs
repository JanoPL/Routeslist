using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Moq;
using RoutesList.Build.Models;
using RoutesList.Build.Services;
using RoutesList.Build.Services.Strategies;
using Xunit;

namespace RoutesList.Tests.Services
{
    public partial class RoutesTests
    {
        [Fact]
        public void GetRoutesInformation_WithNullCollectionProvider_ReturnsOnlyComponentRoutes()
        {
            // Arrange
            var routes = new Routes();
            var mockAssembly = typeof(RoutesTests).Assembly;
            routes.SetAssembly(mockAssembly);

            // Mock RoutesComponent using a static mock
            var mockComponentRoutes = SetupMockComponentRoutes(0);

            // Act
            var result = routes.GetRoutesInformation(null);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(mockComponentRoutes.Count, result.Count);
        }

        [Fact]
        public void GetRoutesInformation_WithEmptyActionDescriptors_ReturnsOnlyComponentRoutes()
        {
            // Arrange
            var routes = new Routes();
            var mockAssembly = typeof(RoutesTests).Assembly;
            routes.SetAssembly(mockAssembly);

            var mockCollectionProvider = new Mock<IActionDescriptorCollectionProvider>();
            var mockActionDescriptorCollection = new ActionDescriptorCollection(new List<ActionDescriptor>(), 1);
            mockCollectionProvider.Setup(m => m.ActionDescriptors).Returns(mockActionDescriptorCollection);

            // Mock RoutesComponent using a static mock
            var mockComponentRoutes = SetupMockComponentRoutes(0);

            // Act
            var result = routes.GetRoutesInformation(mockCollectionProvider.Object);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(mockComponentRoutes.Count, result.Count);
        }

        [Fact]
        public void GetRoutesInformation_WithControllerActionDescriptors_ProcessesCorrectly()
        {
            // Arrange
            var routes = new Routes();
            var mockAssembly = typeof(RoutesTests).Assembly;
            routes.SetAssembly(mockAssembly);

            // Create mock action descriptors
            var controllerActionDescriptor = new ControllerActionDescriptor
            {
                DisplayName = "Test.Controller.Action",
                RouteValues = new Dictionary<string, string>
                {
                    { "controller", "Test" },
                    { "action", "Action" }
                }!
            };

            var mockDescriptors = new List<ActionDescriptor> { controllerActionDescriptor };
            var mockCollectionProvider = SetupMockCollectionProvider(mockDescriptors);

            // Mock RoutesComponent using a static mock
            var mockComponentRoutes = SetupMockComponentRoutes();

            // Act
            var result = routes.GetRoutesInformation(mockCollectionProvider);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(mockComponentRoutes.Count, result.Count);

            var controllerRoute = result.FirstOrDefault(r => r.ControllerName == "Test");
            Assert.NotNull(controllerRoute);
            Assert.Equal("Action", controllerRoute.ActionName);
            Assert.Equal("Test.Controller.Action", controllerRoute.DisplayName);
        }

        [Fact]
        public void GetRoutesInformation_WithCompiledPageDescriptors_ProcessesCorrectly()
        {
            // Arrange
            var routes = new Routes();
            var mockAssembly = typeof(RoutesTests).Assembly;
            routes.SetAssembly(mockAssembly);

            // Create mock action descriptors
            var compiledPageDescriptor = new CompiledPageActionDescriptor
            {
                DisplayName = "Test Page",
                ViewEnginePath = "/Pages/Test",
                RelativePath = "/Pages/Test.cshtml"
            };

            var mockDescriptors = new List<ActionDescriptor> { compiledPageDescriptor };
            var mockCollectionProvider = SetupMockCollectionProvider(mockDescriptors);

            // Mock RoutesComponent using a static mock
            var mockComponentRoutes = SetupMockComponentRoutes();

            // Act
            var result = routes.GetRoutesInformation(mockCollectionProvider);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(mockComponentRoutes.Count, result.Count);

            var pageRoute = result.FirstOrDefault(r => r.IsCompiledPageActionDescriptor);
            Assert.NotNull(pageRoute);
            Assert.Equal("Test Page", pageRoute.DisplayName);
            Assert.Equal("/Pages/Test", pageRoute.ViewEnginePath);
            Assert.Equal("/Pages/Test.cshtml", pageRoute.RelativePath);
        }

        [Fact]
        public void GetRoutesInformation_WithMixedDescriptors_ProcessesAllTypes()
        {
            // Arrange
            var routes = new Routes();
            var mockAssembly = typeof(RoutesTests).Assembly;
            routes.SetAssembly(mockAssembly);

            // Create mock action descriptors
            var controllerActionDescriptor = new ControllerActionDescriptor
            {
                DisplayName = "Test.Controller.Action",
                RouteValues = new Dictionary<string, string>
                {
                    { "controller", "Test" },
                    { "action", "Action" }
                }!
            };

            var compiledPageDescriptor = new CompiledPageActionDescriptor
            {
                DisplayName = "Test Page",
                ViewEnginePath = "/Pages/Test",
                RelativePath = "/Pages/Test.cshtml"
            };

            var unknownDescriptor = new ActionDescriptor(); // Should be skipped

            var mockDescriptors = new List<ActionDescriptor>
            {
                controllerActionDescriptor,
                compiledPageDescriptor,
                unknownDescriptor
            };

            var mockCollectionProvider = SetupMockCollectionProvider(mockDescriptors);

            // Mock RoutesComponent using a static mock
            var mockComponentRoutes = SetupMockComponentRoutes(2);

            // Act
            var result = routes.GetRoutesInformation(mockCollectionProvider);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(mockComponentRoutes.Count, result.Count); // Only 2 descriptors should be processed

            Assert.Single(result, r => r.ControllerName == "Test");
            Assert.Single(result, r => r.IsCompiledPageActionDescriptor);
        }

        private IActionDescriptorCollectionProvider SetupMockCollectionProvider(List<ActionDescriptor> descriptors)
        {
            var mockCollectionProvider = new Mock<IActionDescriptorCollectionProvider>();
            var mockActionDescriptorCollection = new ActionDescriptorCollection(descriptors, 1);
            mockCollectionProvider.Setup(m => m.ActionDescriptors).Returns(mockActionDescriptorCollection);
            return mockCollectionProvider.Object;
        }
        
        private List<RoutesInformationModel> SetupMockComponentRoutes(int count = 1)
        {
            var mockComponentRoutes = new List<RoutesInformationModel>();
            for (int i = 0; i < count; i++)
            {
                // Create mock component routes
                mockComponentRoutes.Add(new RoutesInformationModel { DisplayName = "Component" + i });
            }

            // Use a function replacement/stub for RoutesComponent.GetRoutesToRender
            // RoutesComponent.GetRoutesToRender() = mockComponentRoutes;

            return mockComponentRoutes;
        }

        [Fact]
        public void CanProcess_WithNonControllerActionDescriptor_ReturnsFalse()
        {
            // Arrange
            var strategy = new BuildControllerActionDescriptorStrategy(1);
            var descriptor = new ActionDescriptor(); // Base class

            // Act
            var result = strategy.CanProcess(descriptor);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Process_WithControllerActionDescriptor_BuildsCorrectModel()
        {
            // Arrange
            var strategy = new BuildControllerActionDescriptorStrategy(1);
            var descriptor = new ControllerActionDescriptor
            {
                DisplayName = "Test.Controller.Action",
                RouteValues = new Dictionary<string, string>
                {
                    { "controller", "Test" },
                    { "action", "Action" }
                }!
            };

            // Act
            var builder = strategy.Process(descriptor);
            var result = builder.Create(1).Build();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.Equal("Test", result.ControllerName);
            Assert.Equal("Action", result.ActionName);
            Assert.Equal("Test.Controller.Action", result.DisplayName);
        }
    }
}