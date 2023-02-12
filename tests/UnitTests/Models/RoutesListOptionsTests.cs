using Xunit;
using RoutesList.Build.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.CSharp.RuntimeBinder;

namespace RoutesList.Build.Models.Tests
{
    public class RoutesListOptionsTests
    {
        private RoutesListOptions options = new RoutesListOptions();
        
        [Fact]
        public void GetClassesTest()
        {
            Assert.Equal(options.GetClasses(), "table");
        }

        [Fact]
        public void SetClassesArrayTest()
        {
            IDictionary<string, string[]> dict = new Dictionary<string, string[]>();
            string[] classes = dict["table"] = new string[2] { "table", "table-striped" };

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
            Assert.Equal(options.GetClasses(), new string[] { "table" });
        }

        [Fact]
        public void SetClassesExceptionTest()
        {
            Assert.Throws<RuntimeBinderException>(() => options.SetClasses(123));
        }
    }
}