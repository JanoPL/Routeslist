using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Threading.Tasks;

namespace RoutesList.Gen
{
    public class RoutesListMiddleware
    {
        private readonly RoutesListOptions _options;
        private readonly RequestDelegate _next;

        public RoutesListMiddleware(RoutesListOptions options, RequestDelegate next)
        {
            _options = options ?? new RoutesListOptions();
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (!RequestRoutesList(context.Request)) {
                await _next(context);
                return;
            }

            var httpMethod = context.Request.Method;
            var path = context.Request.Path.Value;

            if (httpMethod == "GET") {
                var relPath = string.IsNullOrEmpty(path) || path.EndsWith("/") ? $"{path.Split('/').Last()}/{_options.Endpoint}" : "";

                if (
                    !string.IsNullOrEmpty(relPath) 
                    && relPath.EndsWith($"/{_options.Endpoint}")
                    && path.EndsWith($"/{_options.Endpoint}")
                ) {
                    await context.Response.WriteAsync("test");
                }

                await _next(context);
            }
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
            response.StatusCode = StatusCodes.Status301MovedPermanently;
            response.Headers["location"] = location;
        }
    }
}
