using Microsoft.AspNetCore.Hosting;
using WebAssemblyProgram = TestBasicBlazorWebAssemblyApp.Program;

namespace RoutesLIst.Integration.Blazor
{
    [Collection("sequential")]
    public class CounterTest
    {
        private readonly PlaywrightFixture _fixture;

        public CounterTest(PlaywrightFixture fixture)
        {
            _fixture = fixture;
        }


        [Fact]
        public async Task Increase_on_click()
        {
            var url = "https://localhost:5001";

            using var hostFactory = new PlayWrightWebApplication<WebAssemblyProgram>();

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
