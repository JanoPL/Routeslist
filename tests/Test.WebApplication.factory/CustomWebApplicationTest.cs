using Web.Application.Factory;

namespace Test.WebApplication.factory
{
    public class CustomWebApplicationTest
    {
        private readonly CustomWebApplication<Program> _application;
        public CustomWebApplicationTest()
        {
            _application = new CustomWebApplication<Program>();
        }

        [Fact]
        public async void WebApplication_Factory_should_create()
        {
            using var client = _application.CreateClient();

            Assert.NotNull(client);
            Assert.IsType<HttpClient>(client);

            using var response = await client.GetAsync("/");

            response.EnsureSuccessStatusCode();
        }
    }
}