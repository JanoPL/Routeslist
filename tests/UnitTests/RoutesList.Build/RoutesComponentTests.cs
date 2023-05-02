using System.Linq;
using Xunit;

namespace RoutesList.Build.Services.Tests
{
    public class RoutesComponentTests
    {
        [Fact]
        public void GetRoutesToRenderNullTest()
        {
            var routes = RoutesComponent.GetRoutesToRender(null);

            Assert.False(routes.Any());
        }
    }
}