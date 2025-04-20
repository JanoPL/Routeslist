using System;
using RoutesList.Build.Extensions;
using UnitTests.TestFiles;
using Xunit;

namespace UnitTests.Extensions
{
    public class EnumExtensionTests
    {
        [Fact]
        public void ToDictionary_ShouldReturnDictionaryWithCorrectKeysAndValues()
        {
            // Arrange & Act
            var result = EnumExtension.ToDictionary(TestEnums.Test1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(3, result.Count);
            
            Assert.True(result.ContainsKey(0));
            Assert.True(result.ContainsKey(1));
            Assert.True(result.ContainsKey(2));
            
            Assert.Equal("Test Name 1", result[0]);
            Assert.Equal("Test Name 2", result[1]);
            Assert.Equal("Test Name 3", result[2]);
        }

        [Fact]
        public void ToDictionary_WithNullValue_ShouldThrowArgumentNullException()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => EnumExtension.ToDictionary<Enum>(null!));
        }

        [Fact]
        public void GetListOfDescription_ShouldReturnListOfDescriptions()
        {
            // Arrange & Act
            var descriptions = EnumExtension.GetListOfDescription<TestEnums>();

            // Assert
            Assert.NotNull(descriptions);
            Assert.Equal(3, descriptions.Count);
            Assert.Equal("Test Description 1", descriptions[0]);
            Assert.Equal("Test Description 2", descriptions[1]);
            Assert.Equal("Test3", descriptions[2]); // Falls back to ToString() when no description
        }

        [Fact]
        public void GetDescription_WithDescriptionAttribute_ShouldReturnDescription()
        {
            // Arrange & Act
            var description = TestEnums.Test1.GetDescription();

            // Assert
            Assert.Equal("Test Description 1", description);
        }

        [Fact]
        public void GetDescription_WithoutDescriptionAttribute_ShouldReturnNull()
        {
            // Arrange & Act
            var description = TestEnums.Test3.GetDescription();

            // Assert
            Assert.Null(description);
        }

        [Fact]
        public void GetDescription_WithNullValue_ShouldReturnNull()
        {
            // Arrange
            Enum? nullEnum = null;

            // Act
            var description = nullEnum.GetDescription();

            // Assert
            Assert.Null(description);
        }
    }
}