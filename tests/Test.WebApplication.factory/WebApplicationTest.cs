using Web.Application.Factory;

namespace Test.WebApplication.factory
{
    public class WebApplicationTest
    {
        [Fact]
        public void WebApplication_Factory_should_create()
        {
            using var webApplication = new CustomWebApplication<Program>();
            var client = webApplication.CreateClient();

            Assert.NotNull(client);
            Assert.IsType<HttpClient>(client);
        }
    }
}