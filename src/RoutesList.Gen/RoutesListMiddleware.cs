using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using RoutesList.Build.Interfaces;
using RoutesList.Build.Models;

namespace RoutesList.Gen
{
    public class RoutesListMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly RoutesListOptions _options;
        private readonly ITableBuilder _tableBuilder;

        public RoutesListMiddleware(RoutesListOptions options, RequestDelegate next, ITableBuilder tableBuilder)
        {
            _options = options ?? new RoutesListOptions();
            _next = next;
            _tableBuilder = tableBuilder;
        }

        public async Task Invoke(HttpContext context)
        {
            if (!RequestRoutesList(context.Request))
            {
                await _next.Invoke(context);
                return;
            }

            var httpMethod = context.Request.Method;
            var path = context.Request.Path.Value;

            if (httpMethod == "GET")
            {
                if (path != null && path.EndsWith("/" + _options.Endpoint + "/json"))
                {
                    await RespondWithJson(context.Response);
                    return;
                }

                if (path != null && path.EndsWith("/" + _options.Endpoint)) await RespondWithHtml(context.Response);
            }
        }

        private static bool RequestRoutesList(HttpRequest request)
        {
            if (request.Method != "GET") return false;

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
                Title = _options.Title,
                CharSet = "UTF-8",
            };
#endif
#if NET6_0_OR_GREATER
            Build.Models.RoutesListOptions buildOptions = new()
            {
                Title = _options.Title,
                CharSet = "UTF-8"
            };
#endif
            var htmlBuilderResult = _tableBuilder.AsyncGenerateTable(_options).GetAwaiter().GetResult();

            await response.WriteAsync(htmlBuilderResult, Encoding.UTF8);
        }

        private async Task RespondWithJson(HttpResponse response)
        {
            response.StatusCode = 200;
            response.ContentType = "application/json; charset=utf-8";

            var htmlBuilderResult = _tableBuilder.AsyncGenerateTable(true, _options).GetAwaiter().GetResult();

            await response.WriteAsync(htmlBuilderResult, Encoding.UTF8);
        }
    }
}