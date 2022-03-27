using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace RouteList.IntegrationTest.Applications
{
    public class TestBasicSiteNet6Razor : WebApplicationFactory<Program>
    {
        private readonly string _environment;
        public TestBasicSiteNet6Razor(string environment = "Development")
        {
            _environment = environment;
        }
        protected override IHost CreateHost(IHostBuilder builder)
        {
            builder.UseEnvironment(_environment);

            builder.ConfigureServices(Services => { });

            return base.CreateHost(builder);
        }
    }
}
