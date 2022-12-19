using System.Net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;

namespace RoutesList.Gen.UnitTest
{
    public class RoutesListViewTest
    {
        [Fact]
        public async Task UseRoutesListTest()
        {
            using var host = await new HostBuilder()
               .ConfigureWebHost(webBuilder => {
                   webBuilder
                       .UseTestServer()
                       .ConfigureServices(services => {
                           services.AddRouting();
                           services.AddMvc();
                           services.AddRoutesList();
                       })
                       .Configure(app => {
                           app.UseRouting();
                           app.UseRoutesList();
                       });
               })
               .StartAsync();

            var response = await host.GetTestClient().GetAsync("/routes");
        }

        [Fact]
        public async Task UseRoutesListJsonTest()
        {
            using var host = await new HostBuilder()
               .ConfigureWebHost(webBuilder => {
                   webBuilder
                       .UseTestServer()
                       .ConfigureServices(services => {
                           services.AddRouting();
                           services.AddMvc();
                           services.AddRoutesList();
                       })
                       .Configure(app => {
                           app.UseRouting();
                           app.UseRoutesList();
                       });
               })
               .StartAsync();

            var response = await host.GetTestClient().GetAsync("/routes/json");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
