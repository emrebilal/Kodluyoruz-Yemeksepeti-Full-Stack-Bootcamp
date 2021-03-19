using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LoggingMiddleware.Middlewares
{
    public class LogResponseMiddleware
    {
        private readonly RequestDelegate _next;

        public LogResponseMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var bodyStream = context.Response.Body;

            var responseBodyStream = new MemoryStream();
            context.Response.Body = responseBodyStream;

            await _next(context);
            responseBodyStream.Seek(0, SeekOrigin.Begin);

            var url = UriHelper.GetDisplayUrl(context.Request);
            var responseBody = new StreamReader(responseBodyStream).ReadToEnd();

            var response = context.Response;

            string log = $"{DateTime.Now.ToString()} | Request Url: {url}" + Environment.NewLine +
                         $"Status Code: '{response.StatusCode}', Response Body: '{responseBody}',  Content Type: '{response.ContentType}'" + Environment.NewLine;

            File.AppendAllText("Logs/ResponseLog.txt", log + Environment.NewLine);

            responseBodyStream.Seek(0, SeekOrigin.Begin);
            await responseBodyStream.CopyToAsync(bodyStream);
        }
    }
}
