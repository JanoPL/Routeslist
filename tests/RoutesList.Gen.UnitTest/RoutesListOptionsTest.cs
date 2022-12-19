using System.Net;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.CSharp.RuntimeBinder;
using Microsoft.Extensions.Hosting;

namespace RoutesList.Gen.UnitTest
{
    public class RoutesListOptionsTest
    {
        [Fact]
        public async Task OptionsEndpointTest()
        {
            string endpoint = "/testEndpoint";

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
                           app.UseRoutesList(options => {
                               options.Endpoint = endpoint;
                           });
                       });
               })
               .StartAsync();

            var response = await host.GetTestClient().GetAsync(endpoint);

            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task OptionsTitle()
        {
            string title = "testTitle";

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
                           app.UseRoutesList(options => {
                               options.Tittle = title;
                           });
                       });
               })
               .StartAsync();

            var response = await host.GetTestClient().GetAsync("/routes");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task OptionsTableClassesTest()
        {
            IDictionary<string, string[]> dict = new Dictionary<string, string[]>();
            string[] classes = dict["table"] = new string[2] { "table", "table-striped" };

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
                           app.UseRoutesList(options => {
                               options.SetTableClasses(classes);
                           });
                       });
               })
               .StartAsync();

            var response = await host.GetTestClient().GetAsync("/routes");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task OptionsTableClassesSingleValueTest()
        {
            string classes = "table";

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
                           app.UseRoutesList(options => {
                               options.SetTableClasses(classes);
                           });
                       });
               })
               .StartAsync();

            var response = await host.GetTestClient().GetAsync("/routes");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task OptionsTableClassesNullValueTest()
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
                          app.UseRoutesList(options => {
                              options.SetTableClasses(null);
                          });
                      });
              })
              .StartAsync();

            var response = await host.GetTestClient().GetAsync("/routes");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public void OptionsTableClassesExceptionTest()
        {
            var routesListOptions = new RoutesListOptions();
            Assert.Throws<RuntimeBinderException>(() => routesListOptions.SetTableClasses(123));
        }
    }
}
