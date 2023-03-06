using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;
using Xunit.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

namespace Web.Application.Factory
{
    public class BlazorWebApplication<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        public ITestOutputHelper? OutputHelper { get; set; }

        protected override IHostBuilder? CreateHostBuilder()
        {
            IServerSideBlazorBuilder blazorBuilderService = base.Services.GetRequiredService<IServerSideBlazorBuilder>();

            var builder = base.CreateHostBuilder();

            if (builder != null) {
                builder.UseEnvironment("Development");

                if (OutputHelper != null) {
                    builder.ConfigureLogging(logging => {
                        logging.ClearProviders();
                        logging.AddXUnit(OutputHelper);
                    });
                }

                builder.ConfigureWebHost(
                    webHostBuilder => {
                        webHostBuilder.UseStaticWebAssets();
                        webHostBuilder.ConfigureTestServices(services => {
                            services.AddSingleton(_ => CreateDefaultClient());
                        });
                    }
                );
            }

            return builder;
        }
    }
}
