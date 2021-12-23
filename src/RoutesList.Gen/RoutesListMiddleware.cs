using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using RoutesList.Interfaces;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace RoutesList.Gen
{
    public class RoutesListMiddleware
    {
        private readonly RoutesListOptions _options;
        private readonly StaticFileMiddleware _staticFileMiddleware;
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
                    return;
                }
            }

            await _staticFileMiddleware.Invoke(context);
        }

        private bool RequestRoutesList(HttpRequest request)
        {
            if (request.Method != "GET") {
                return false;
            }

            return true;
        }

        private void Redirect(HttpResponse response, string location)
        {
            response.StatusCode = 301;
            response.Headers["location"] = location;
        }
        private async Task RespondWithHtml(HttpResponse response)
        {
            response.StatusCode = 200;
            response.ContentType = "text/html";

            Build.Models.RoutesListOptions buildOptions = new Build.Models.RoutesListOptions() {
                Tittle = _options.Tittle,
                CharSet = "UTF-8"
            };

            var htmlBuilderResult = _tableBuilder.AsyncGenerateTable(false, buildOptions).GetAwaiter().GetResult();

            await response.WriteAsync(htmlBuilderResult.ToString(), Encoding.UTF8);
        }

        private async Task RespondWithJson(HttpResponse response)
        {
            response.StatusCode = 200;
            response.ContentType = "application/json; charset=utf-8";

            var htmlBuilderResult = _tableBuilder.AsyncGenerateTable(true).GetAwaiter().GetResult();

            await response.WriteAsync(htmlBuilderResult, Encoding.UTF8);
        }
    }
}
