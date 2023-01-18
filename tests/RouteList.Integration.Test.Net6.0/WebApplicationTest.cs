namespace RouteList.Integration.Test.Net6._0
{
    public class WebApplicationTest
    {
        private readonly CustomWebApplication<TestBasicSiteASPNet6.Program> _application;
        public WebApplicationTest()
        {
            _application = new CustomWebApplication<TestBasicSiteASPNet6.Program>();
        }

        [Theory]
        [InlineData("/", "text/html; charset=utf-8")]
        [InlineData("/Privacy", "text/html; charset=utf-8")]
        [InlineData("/routes", "text/html")]
        [InlineData("/routes/json", "application/json; charset=utf-8")]
        public async Task ResponseTest(string url, string contentType)
        {
            using var client = _application.CreateClient();
            using HttpResponseMessage? response = await client.GetAsync(url);

            response.EnsureSuccessStatusCode();

            Assert.NotNull(response);

            Assert.Equal(
                contentType,
                response?.Content?.Headers?.ContentType?.ToString()
            );
        }
    }
}