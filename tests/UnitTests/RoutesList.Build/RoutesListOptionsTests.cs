using System.Collections.Generic;
using System.Reflection;
using Microsoft.CSharp.RuntimeBinder;
#if !NET6_0_OR_GREATER
using Xunit;
#endif

namespace RoutesList.Build.Models.Tests
{
    public class RoutesListOptionsTests
    {
        private readonly RoutesListOptions options = new RoutesListOptions();

        [Fact]
        public void GetClassesTest()
        {
            Assert.Equal(options.GetClasses(), "table");
        }

        [Fact]
        public void SetClassesArrayTest()
        {
            IDictionary<string, string[]> dict = new Dictionary<string, string[]>();
            string[] classes = dict["table"] = new[] { "table", "table-striped" };

            options.SetClasses(classes);

            Assert.Equal(options.GetClasses(), classes);
        }

        [Fact]
        public void SetClassesStringTest()
        {
            options.SetClasses("table");

            Assert.Equal(options.GetClasses(), "table");
        }

        [Fact]
        public void SetClassesNullTest()
        {
            options.SetClasses(null);
            Assert.Equal(options.GetClasses(), new[] { "table" });
        }

        [Fact]
        public void SetClassesExceptionTest()
        {
            Assert.Throws<RuntimeBinderException>(() => options.SetClasses(123));
        }

        [Fact]
        public void SetAppAssembly()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();

            options.SetAppAssembly(assembly);

            Assert.Equal(options.GetAppAssembly(), assembly);
        }
    }
}