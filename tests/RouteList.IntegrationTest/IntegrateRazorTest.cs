using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using RouteList.IntegrationTest.Applications;
using Xunit;

namespace RouteList.IntegrationTest
{
    public class IntegrateRazorTest
    {
        //private readonly WebApplicationFactory<Program> _factory;

        public IntegrateRazorTest()
        {
            using var application = new TestBasicSiteNet6Razor();
        }

        [Theory]
        [InlineData("/")]
        [InlineData("/Privacy")]
        public async void ResponseTest(string url)
        {
            //var client = _factory.CreateClient();

            using var application = new TestBasicSiteNet6Razor();
            using var client = application.CreateClient();
            using var response = await client.GetAsync(url);

            response.EnsureSuccessStatusCode();
            Assert.Equal(
                "text/html; charset=utf-8",
                response.Content.Headers.ContentType.ToString()
            );
        }

        [Theory]
        [InlineData("/routes")]
        public async void EndpointDefaultTest(string url)
        {
            using var application = new TestBasicSiteNet6Razor();
            using var client = application.CreateClient();
            using var response = await client.GetAsync(url);

            response.EnsureSuccessStatusCode();

            Assert.Equal(
                "text/html",
                response.Content.Headers.ContentType.ToString()
            );
        }

        [Theory]
        [InlineData("/routes/json")]
        public async void EndpointDefaultJsonTest(string url)
        {
            using var application = new TestBasicSiteNet6Razor();
            using var client = application.CreateClient();
            using var response = await client.GetAsync(url);

            response.EnsureSuccessStatusCode();

            Assert.Equal(
                "application/json; charset=utf-8",
                response.Content.Headers.ContentType.ToString()
            );
        }
    }
}
