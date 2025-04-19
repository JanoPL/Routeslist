using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using RoutesList.Build.Interfaces;
using RoutesList.Build.Models;

namespace RoutesList.Gen
{
    /// <summary>
    /// Middleware component that handles requests for displaying routes information in HTML or JSON format.
    /// </summary>
    public class RoutesListMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly RoutesListOptions _options;
        private readonly ITableBuilder _tableBuilder;

        /// <summary>
        /// Initializes a new instance of the <see cref="RoutesListMiddleware"/> class.
        /// </summary>
        /// <param name="options">The options for configuring the routes list.</param>
        /// <param name="next">The next middleware in the pipeline.</param>
        /// <param name="tableBuilder">The service for building the routes table.</param>
        public RoutesListMiddleware(RoutesListOptions options, RequestDelegate next, ITableBuilder tableBuilder)
        {
            _options = options ?? new RoutesListOptions();
            _next = next;
            _tableBuilder = tableBuilder;
        }

        /// <summary>
        /// Processes an HTTP request to either display routes information or continue the pipeline.
        /// </summary>
        /// <param name="context">The HTTP context for the request.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
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

        /// <summary>
        /// Determines if the request is for the routes list.
        /// </summary>
        /// <param name="request">The HTTP request to check.</param>
        /// <returns>True if the request is for routes list, otherwise false.</returns>
        private static bool RequestRoutesList(HttpRequest request)
        {
            if (request.Method != "GET") return false;

            return true;
        }

        /// <summary>
        /// Performs a redirect to the specified location.
        /// </summary>
        /// <param name="response">The HTTP response.</param>
        /// <param name="location">The location to redirect to.</param>
        private static void Redirect(HttpResponse response, string location)
        {
            response.StatusCode = 301;
            response.Headers["location"] = location;
        }

        /// <summary>
        /// Responds with HTML format of the routes list.
        /// </summary>
        /// <param name="response">The HTTP response to write to.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
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

        /// <summary>
        /// Responds with JSON format of the routes list.
        /// </summary>
        /// <param name="response">The HTTP response to write to.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        private async Task RespondWithJson(HttpResponse response)
        {
            response.StatusCode = 200;
            response.ContentType = "application/json; charset=utf-8";

            var htmlBuilderResult = _tableBuilder.AsyncGenerateTable(true, _options).GetAwaiter().GetResult();

            await response.WriteAsync(htmlBuilderResult, Encoding.UTF8);
        }
    }
}