using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RoutesList.Build.Services.Strategies;
using Xunit;

namespace RoutesList.Tests.Services
{
    public class BuildCompiledPageDescriptorStrategyTests
    {
        [Fact]
        public void CanProcess_WithCompiledPageActionDescriptor_ReturnsTrue()
        {
            // Arrange
            var descriptor = new CompiledPageActionDescriptor();
            var items = new List<ActionDescriptor> { descriptor };
            var strategy = new BuildCompiledPageDescriptorStrategy(1, items);

            // Act
            var result = strategy.CanProcess(descriptor);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public void CanProcess_WithNonCompiledPageActionDescriptor_ReturnsFalse()
        {
            // Arrange
            var pageDescriptor = new CompiledPageActionDescriptor();
            var items = new List<ActionDescriptor> { pageDescriptor };
            var strategy = new BuildCompiledPageDescriptorStrategy(1, items);

            var nonPageDescriptor = new ActionDescriptor(); // Base class

            // Act
            var result = strategy.CanProcess(nonPageDescriptor);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void Process_WithCompiledPageActionDescriptor_BuildsCorrectModel()
        {
            // Arrange
            var descriptor = new CompiledPageActionDescriptor
            {
                DisplayName = "Test Page",
                ViewEnginePath = "/Pages/Test",
                RelativePath = "/Pages/Test.cshtml"
            };

            var items = new List<ActionDescriptor> { descriptor };
            var strategy = new BuildCompiledPageDescriptorStrategy(1, items);

            // Act
            var builder = strategy.Process(descriptor);
            var result = builder.Build();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            Assert.True(result.IsCompiledPageActionDescriptor);
            Assert.Equal("Test Page", result.DisplayName);
            Assert.Equal("/Pages/Test", result.ViewEnginePath);
            Assert.Equal("/Pages/Test.cshtml", result.RelativePath);
        }
    }
}