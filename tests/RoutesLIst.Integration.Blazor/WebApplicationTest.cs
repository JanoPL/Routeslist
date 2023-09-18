using Bunit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RoutesList.Build.Services;
using Xunit.Abstractions;

using BlazorServerProgram = TestBasicBlazorServer.Program;


namespace RoutesLIst.Integration.Blazor
{
    public class WebApplicationTest
    {
        private readonly CustomWebApplication<BlazorServerProgram> _application;

        public WebApplicationTest()
        {
            _application = new CustomWebApplication<BlazorServerProgram>();
        }

        [Theory]
        [InlineData("/", "text/html; charset=utf-8")]
        [InlineData("/Privacy", "text/html; charset=utf-8")]
        [InlineData("/testing", "text/html; charset=utf-8")]
        [InlineData("/routes", "text/html")]
        [InlineData("/routes/json", "application/json; charset=utf-8")]
        public async Task ResponseTest(string url, string contentType)
        {
            using var client = _application.CreateClient();
            using var response = await client.GetAsync(url);

            response.EnsureSuccessStatusCode();
            Assert.Equal(
                contentType,
                response?.Content?.Headers?.ContentType?.ToString()
            );
        }
    }
}