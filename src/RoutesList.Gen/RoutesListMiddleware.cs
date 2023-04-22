using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using RoutesList.Interfaces;

namespace RoutesList.Gen
{
    public class RoutesListMiddleware
    {
        private readonly RoutesListOptions _options;
        private readonly ITableBuilder _tableBuilder;
        private readonly RequestDelegate _next;

        public RoutesListMiddleware(RoutesListOptions options, RequestDelegate next, ITableBuilder tableBuilder)
        {
            _options = options ?? new RoutesListOptions();
            _next = next;
            _tableBuilder = tableBuilder;
        }

        public async Task Invoke(HttpContext context)
        {
            if (!RequestRoutesList(context.Request)) {
                await _next.Invoke(context);
                return;
            }

            var httpMethod = context.Request.Method;
            var path = context.Request.Path.Value;

            if (httpMethod == "GET") {
                if (path.EndsWith($"/{_options.Endpoint}/json")) {
                    await RespondWithJson(context.Response);
                    return;
                }

                if (path.EndsWith($"/{_options.Endpoint}")) {
                    await RespondWithHtml(context.Response);
                }
            }
        }

        private static bool RequestRoutesList(HttpRequest request)
        {
            if (request.Method != "GET") {
                return false;
            }

            return true;
        }

        private static void Redirect(HttpResponse response, string location)
        {
            response.StatusCode = 301;
            response.Headers["location"] = location;
        }
        private async Task RespondWithHtml(HttpResponse response)
        {
            response.StatusCode = 200;
            response.ContentType = "text/html";
#if NETCOREAPP3_1 || NET5_0
            Build.Models.RoutesListOptions buildOptions = new Build.Models.RoutesListOptions {
                Tittle = _options.Tittle,
                CharSet = "UTF-8",
            };
#endif
#if NET6_0_OR_GREATER
            Build.Models.RoutesListOptions buildOptions = new() {
                Tittle = _options.Tittle,
                CharSet = "UTF-8",
            };
#endif
            buildOptions.SetClasses(_options.GetTableClasses());
            buildOptions.SetAppAssembly(_options.GetAppAssembly());

            string htmlBuilderResult = _tableBuilder.AsyncGenerateTable(buildOptions).GetAwaiter().GetResult();

            await response.WriteAsync(htmlBuilderResult.ToString(), Encoding.UTF8);
        }

        private async Task RespondWithJson(HttpResponse response)
        {
            response.StatusCode = 200;
            response.ContentType = "application/json; charset=utf-8";

#if NETCOREAPP3_1 || NET5_0
            Build.Models.RoutesListOptions buildOptions = new Build.Models.RoutesListOptions {
                Tittle = _options.Tittle,
                CharSet = "UTF-8",
            };
#endif
#if NET6_0_OR_GREATER
            Build.Models.RoutesListOptions buildOptions = new() {
                Tittle = _options.Tittle,
                CharSet = "UTF-8",
            };
#endif

            buildOptions.SetAppAssembly (_options.GetAppAssembly());

            string htmlBuilderResult = _tableBuilder.AsyncGenerateTable(isJson: true, buildOptions).GetAwaiter().GetResult();

            await response.WriteAsync(htmlBuilderResult, Encoding.UTF8);
        }
    }
}
