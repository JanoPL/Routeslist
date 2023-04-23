using Bunit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RoutesList.Build.Services;
using Xunit.Abstractions;

namespace RoutesLIst.Integration.Blazor
{
    public class WebApplicationTest
    {
        private readonly CustomWebApplication<TestBasicBlazorServer.Program> _application;

        public WebApplicationTest()
        {
            _application = new CustomWebApplication<TestBasicBlazorServer.Program>();

            var config = _application.Services.GetRequiredService<IConfiguration>();
        }

        public static IEnumerable<object[]>? GetPages()
            => RoutesComponent
                .GetRoutesToRender(typeof(TestBasicBlazorServer.App).Assembly)
                ?.Select(config => new object[] { config });

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

        //TODO: add component test with bunit
    }
}