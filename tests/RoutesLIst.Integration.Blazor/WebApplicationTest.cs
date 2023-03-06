using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RoutesList.Build.Services;
using Xunit.Abstractions;

namespace RoutesLIst.Integration.Blazor
{
    public class WebApplicationTest
    {
        private readonly CustomWebApplication<TestBasicBlazorServer.Program> _application;
        private readonly ITestOutputHelper _testOutputHelper;
        private readonly string _outPutPath;

        public WebApplicationTest(ITestOutputHelper testOutputHelper)
        {
            _application = new CustomWebApplication<TestBasicBlazorServer.Program>();
            _testOutputHelper = testOutputHelper;

            var config = _application.Services.GetRequiredService<IConfiguration>();
            _outPutPath = config["RenderOutputDirectory"] ?? ".RenderOutput";
        }

        [Theory]
        [InlineData("/", "text/html; charset=utf-8")]
        [InlineData("/Privacy", "text/html; charset=utf-8")]
        [InlineData("/testing", "text/html; charset=utf-8")]
        [InlineData("/routes", "text/html")]
        [InlineData("/routes/json", "application/json; charset=utf-8")]
        public async Task ResponseTest(string url, string contentType)
        {
            using var client = _application.CreateClient();
            using var response = await client.GetAsync(url);

            response.EnsureSuccessStatusCode();
            Assert.Equal(
                contentType,
                response?.Content?.Headers?.ContentType?.ToString()
            );
        }

        public static IEnumerable<object[]> GetPages() 
            => RoutesComponent
                .GetRoutesToRender(typeof(TestBasicBlazorServer.App).Assembly)
                .Select(config => new object[] { config });

        [Theory]
        [MemberData(nameof(GetPages))]
        public async Task RenderComponentTest(string route)
        {
            //using var client = _blazorApp.CreateClient();
            using var client = _application.CreateClient();
            var renderPath = route.Substring(1);
            var relativePath = Path.Combine(_outPutPath, renderPath);
            var outputDirectory = Path.GetFullPath(relativePath);

            _testOutputHelper.WriteLine($"creating directory '{outputDirectory}'");
            Directory.CreateDirectory(outputDirectory);

            var fileName = Path.Combine(outputDirectory, "index.html");
            var result = await client.GetStreamAsync(route);

            _testOutputHelper.WriteLine($"Writing content to '{fileName}'");
            using (var file = new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite, FileShare.None)) {
                await result.CopyToAsync(file);
            }

            _testOutputHelper.WriteLine($"Pre rendering complete");
        }
    }
}