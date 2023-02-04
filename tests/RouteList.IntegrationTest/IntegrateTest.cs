#if NETCOREAPP3_1 || NET5_0
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
#endif

namespace RouteList.IntegrationTest
{
    public class IntegrateTest : IClassFixture<WebApplicationFactory<TestBasicSite.Startup>>
    {
        private readonly WebApplicationFactory<TestBasicSite.Startup> _factory;

        public IntegrateTest(WebApplicationFactory<TestBasicSite.Startup> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("/", "text/html; charset=utf-8")]
        [InlineData("/home", "text/html; charset=utf-8")]
        [InlineData("/Privacy", "text/html; charset=utf-8")]
        [InlineData("/routes", "text/html")]
        [InlineData("/routes/json", "application/json; charset=utf-8")]
        public async Task ResponseTest(string url, string contentType)
        {
            var client = _factory.CreateClient();

            HttpResponseMessage? response = await client.GetAsync(url);

            Assert.NotNull(response);

            response.EnsureSuccessStatusCode();

            Assert.Equal(
                contentType,
                response?.Content?.Headers?.ContentType?.ToString()
            );
        }
    }
}