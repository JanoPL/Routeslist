using System.Collections.Generic;
using System.Reflection;
using Microsoft.CSharp.RuntimeBinder;
using RoutesList.Build.Models;

#if !NET6_0_OR_GREATER
using Xunit;
#endif

namespace UnitTests.RoutesList.Gen
{
    public class RoutesListOptionsTests
    {
        private readonly RoutesListOptions _options = new RoutesListOptions();
        [Fact]
        public void GetTableClassesTest()
        {
            Assert.Equal(_options.Classes, "table");
        }

        [Fact]
        public void SetTableClassesArrayTest()
        {
            IDictionary<string, string[]> dict = new Dictionary<string, string[]>();
            string[] classes = dict["table"] = new[] { "table", "table-striped" };

            _options.Classes = classes;

            Assert.Equal(classes, _options.Classes);
        }

        [Fact]
        public void SetTableClassesStringTest()
        {
            _options.Classes = "table";

            Assert.Equal("table", _options.Classes);
        }

        [Fact]
        public void SetTableClassesNullTest()
        {
            _options.Classes = null;
            Assert.Equal(new[] { "table" }, _options.Classes);
        }

        [Fact]
        public void SetTableClassesExceptionTest()
        {
            Assert.Throws<RuntimeBinderException>(() => _options.Classes = 123);
        }

        [Fact]
        public void SetAppAssembly()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();

            _options.AppAssembly = assembly;

            Assert.Equal(assembly, _options.AppAssembly);
        }
    }
}