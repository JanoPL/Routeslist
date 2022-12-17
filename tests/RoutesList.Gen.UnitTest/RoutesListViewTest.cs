using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Hosting;

namespace RoutesList.Gen.UnitTest
{
    public class RoutesListViewTest
    {
        //private static (ServiceCollection services, IConfigurationRoot configuration) GetServiceConfiguration()
        //{
        //    var services = new ServiceCollection();
        //    var configuration = new ConfigurationBuilder().Build();

        //    services.AddOptions();
        //    services.AddRouting();
        //    services.AddRoutesList();

        //    return (services, configuration);
        //}

        //private static ApplicationBuilder GetApplicationBuilder()
        //{
        //    (ServiceCollection services, IConfigurationRoot configuration) = GetServiceConfiguration();

        //    var applicationBuilder = new ApplicationBuilder(services.BuildServiceProvider());
        //    applicationBuilder.UseRouting();

        //    return applicationBuilder;
        //}

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
        }
    }
}
