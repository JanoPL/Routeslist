namespace RouteList.Integration.Test.Net6._0
{
    public class WebApplicationTest
    {
        public readonly CustomWebApplication<TestBasicSiteASPNet6.Program> _application;
        public WebApplicationTest()
        {
            _application = new CustomWebApplication<TestBasicSiteASPNet6.Program>();
        }

        [Theory]
        [InlineData("/")]
        [InlineData("/Privacy")]
        public async void ResponseTest(string url)
        {
            using var client = _application.CreateClient();
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
            using var client = _application.CreateClient();
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
            using var client = _application.CreateClient();
            using var response = await client.GetAsync(url);

            response.EnsureSuccessStatusCode();

            Assert.Equal(
                "application/json; charset=utf-8",
                response.Content.Headers.ContentType.ToString()
            );
        }
    }
}