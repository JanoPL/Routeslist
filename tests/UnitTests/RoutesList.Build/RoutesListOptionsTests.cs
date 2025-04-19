using System.Reflection;
using Microsoft.CSharp.RuntimeBinder;
using RoutesList.Build.Models;
#if !NET6_0_OR_GREATER
using Xunit;
#endif

namespace UnitTests.RoutesList.Build
{
    public class RoutesListOptionsTests
    {
        private readonly RoutesListOptions _options = new RoutesListOptions();

        [Fact]
        [Trait("Category", "ClassesProperty")]
        public void When_SetClassesToNull_Then_ShouldReturnDefaultTable()
        {
            _options.Classes = null;
            Assert.Equal(new[] { "table" }, _options.Classes);
        }

        [Fact]
        [Trait("Category", "ClassesProperty")]
        public void When_SetInvalidType_Then_ShouldThrowRuntimeBinderException()
        {
            Assert.Throws<RuntimeBinderException>(() => _options.Classes = 123);
        }

        [Fact]
        public void AppAssemblyPropertyTest()
        {
            var assembly = Assembly.GetExecutingAssembly();
            _options.AppAssembly = assembly;
            Assert.Equal(assembly, _options.AppAssembly);
        }

        [Fact]
        [Trait("Category", "ClassesProperty")]
        public void When_SwitchingBetweenStringAndArray_Then_ShouldPreserveValues()
        {
            _options.Classes = "table table-striped";
            Assert.Equal("table table-striped", _options.Classes);

            var classArray = new[] { "table", "table-bordered" };
            _options.Classes = classArray;
            Assert.Equal(classArray, _options.Classes);
        }

        [Trait("Category", "ClassesProperty")]
        public class ClassesPropertyTests
        {
            private readonly RoutesListOptions _options = new RoutesListOptions();

            [Fact]
            public void When_NewInstance_Then_DefaultClassesPropertyShouldBeTable()
            {
                Assert.Equal("table", _options.Classes);
            }

            [Theory]
            [InlineData("table")]
            [InlineData("table table-striped")]
            [InlineData("custom-table")]
            [Trait("Category", "ClassesProperty")]
            public void When_SetValidClassString_Then_ShouldReturnSameValue(string className)
            {
                _options.Classes = className;
                Assert.Equal(className, _options.Classes);
            }

            [Fact]
            [Trait("Category", "ClassesProperty")]
            public void When_SetClassesArray_Then_ShouldReturnSameArray()
            {
                var classes = new[] { "table", "table-striped" };
                _options.Classes = classes;
                Assert.Equal(classes, _options.Classes);
            }
        }
    }
}