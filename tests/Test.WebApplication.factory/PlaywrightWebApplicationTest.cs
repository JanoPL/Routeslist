using Microsoft.AspNetCore.Hosting;
using Web.Application.Factory;
using Program1 = TestBasicBlazorWebAssemblyApp.Program;

namespace Test.WebApplication.factory
{
    [Collection(PlaywrightFixture.PlaywrightCollection)]
    public class PlaywrightWebApplicationTest
    {
        private readonly PlaywrightFixture _fixture;

        public PlaywrightWebApplicationTest(PlaywrightFixture fixture)
        {
            _fixture = fixture;
        }


        [Fact]
        public async Task WebApplication_Factory_Blazor_should_create()
        {
            var url = "https://localhost:5002";

            using var hostFactory = new PlayWrightWebApplication<Program1>();

            hostFactory
                .WithWebHostBuilder(builder => {
                    builder.UseUrls(url);
                    builder.ConfigureServices(services => { });
                    builder.ConfigureAppConfiguration((app, conf) => { });
                }).CreateDefaultClient();

            await _fixture.GotoPageAsync(
                url,
                async (page) => {
                    // Apply the test logic here

                    // Click text=Home
                    await page.Locator("text=Home").ClickAsync();
                    await page.WaitForURLAsync($"{url}/");
                    // Click text=Hello, world!
                    await page.Locator("text=Hello, world!").IsVisibleAsync();

                    // Click text=Counter
                    await page.Locator("text=Counter").ClickAsync();
                    await page.WaitForURLAsync($"{url}/counter");
                    // Click h1:has-text("Counter")
                    await page.Locator("h1:has-text(\"Counter\")").IsVisibleAsync();
                    // Click text=Click me
                    await page.Locator("text=Click me").ClickAsync();
                    // Click text=Current count: 1
                    await page.Locator("text=Current count: 1").IsVisibleAsync();
                    // Click text=Click me
                    await page.Locator("text=Click me").ClickAsync();
                    // Click text=Current count: 2
                    await page.Locator("text=Current count: 2").IsVisibleAsync();
                },
                BrowserEnums.Chromium
            );
        }
    }
}
