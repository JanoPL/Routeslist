using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RutesList.Gen
{
    public class RoutesListMiddleware
    {
        private readonly RoutesListOptions _options;
        
        public RoutesListMiddleware(RoutesListOptions options)
        {
            _options = options ?? new RoutesListOptions();
        }

        public async Task Invoke(HttpContext context)
        {
            var httpMethod = context.Request.Method;
            var path = context.Request.Path.Value;

            if (httpMethod == "GET") {
                var relPath = string.IsNullOrEmpty(path) || path.EndsWith("/") ? $"{path.Split('/').Last()}/{_options.Endpoint}" : "";

                if (!string.IsNullOrEmpty(relPath)) {
                    Redirect(context.Response, relPath);
                }

                return;
            }
        }

        private void Redirect(HttpResponse response, string location)
        {
            response.StatusCode = StatusCodes.Status301MovedPermanently;
            response.Headers["location"] = location;
        }
    }
}
