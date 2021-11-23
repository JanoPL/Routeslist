using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

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
        [InlineData("/")]
        [InlineData("/home")]
        [InlineData("/Home/Privacy")]
        public async void ResponseTest(string url)
        {
            var client  = _factory.CreateClient();

            var response = await client.GetAsync(url);

            response.EnsureSuccessStatusCode();
            Assert.Equal(
                "text/html; charset=utf-8",
                response.Content.Headers.ContentType.ToString()
            );
        }
    }
}