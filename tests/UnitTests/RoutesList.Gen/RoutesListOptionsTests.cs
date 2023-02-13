using System.Collections.Generic;
using Microsoft.CSharp.RuntimeBinder;
#if !NET6_0_OR_GREATER
using Xunit;
#endif

namespace RoutesList.Gen.Tests
{
    public class RoutesListOptionsTests
    {
        private readonly RoutesListOptions options = new RoutesListOptions();
        [Fact]
        public void GetTableClassesTest()
        {
            Assert.Equal(options.GetTableClasses(), "table");
        }

        [Fact]
        public void SetTableClassesArrayTest()
        {
            IDictionary<string, string[]> dict = new Dictionary<string, string[]>();
            string[] classes = dict["table"] = new[] { "table", "table-striped" };

            options.SetTableClasses(classes);

            Assert.Equal(options.GetTableClasses(), classes);
        }

        [Fact]
        public void SetTableClassesStringTest()
        {
            options.SetTableClasses("table");

            Assert.Equal(options.GetTableClasses(), "table");
        }

        [Fact]
        public void SetTableClassesNullTest()
        {
            options.SetTableClasses(null);
            Assert.Equal(options.GetTableClasses(), new[] { "table" });
        }

        [Fact]
        public void SetTableClassesExceptionTest()
        {
            Assert.Throws<RuntimeBinderException>(() => options.SetTableClasses(123));
        }
    }
}