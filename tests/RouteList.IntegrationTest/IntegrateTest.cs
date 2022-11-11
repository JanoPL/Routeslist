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
        [InlineData("/Privacy")]
        public async Task ResponseTest(string url)
        {
            var client = _factory.CreateClient();

            HttpResponseMessage? response = await client.GetAsync(url);

            Assert.NotNull(response);

            response.EnsureSuccessStatusCode();

            Assert.Equal(
                "text/html; charset=utf-8",
                response?.Content?.Headers?.ContentType?.ToString()
            );
        }

        [Theory]
        [InlineData("/routes")]
        public async Task EndpointDefaultTest(string url)
        {
            using var client = _factory.CreateClient();
            using HttpResponseMessage? response = await client.GetAsync(url);

            Assert.NotNull(response);

            response.EnsureSuccessStatusCode();

            Assert.Equal(
                "text/html",
                response?.Content?.Headers?.ContentType?.ToString()
            );
        }

        [Theory]
        [InlineData("/routes/json")]
        public async Task EndpointDefaultJsonTest(string url)
        {
            using var client = _factory.CreateClient();
            using HttpResponseMessage? response = await client.GetAsync(url);

            Assert.NotNull(response);

            response.EnsureSuccessStatusCode();

            Assert.Equal(
                "application/json; charset=utf-8",
                response?.Content?.Headers?.ContentType?.ToString()
            );
        }
    }
}