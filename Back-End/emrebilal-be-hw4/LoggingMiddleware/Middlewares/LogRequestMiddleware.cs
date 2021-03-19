using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using System;
using System.IO;
using System.Threading.Tasks;
using UAParser;

namespace LoggingMiddleware.Middlewares
{
    public class LogRequestMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHttpContextAccessor _accessor;

        public LogRequestMiddleware(RequestDelegate next, IHttpContextAccessor accessor)
        {
            _next = next;
            _accessor = accessor;
        }

        public async Task Invoke(HttpContext context)
        {
            var requestBodyStream = new MemoryStream();
            var originalRequestBody = context.Request.Body;

            await context.Request.Body.CopyToAsync(requestBodyStream);
            requestBodyStream.Seek(0, SeekOrigin.Begin);

            var url = UriHelper.GetDisplayUrl(context.Request);
            var requestBodyText = new StreamReader(requestBodyStream).ReadToEnd();

            var request = context.Request;
            var ip = _accessor.HttpContext.Connection.RemoteIpAddress;

            var userAgent = request.Headers["User-Agent"];
            var uaParser = Parser.GetDefault();
            ClientInfo c = uaParser.Parse(userAgent);

            string log = $"{DateTime.Now.ToString()} | Request Url: {url}" + Environment.NewLine +
                         $"Request Method: {request.Method}, Scheme: {request.Scheme}, Host: {request.Host}, Path: '{request.Path}', Query String: '{request.QueryString}', Body: '{requestBodyText}' " +
                         $"UserAgent: {c.UA} ({c.Device}/{c.OS}) - Client Ip: {ip}" + Environment.NewLine;

            File.AppendAllText("Logs/RequestLog.txt", log + Environment.NewLine);

            requestBodyStream.Seek(0, SeekOrigin.Begin);
            context.Request.Body = requestBodyStream;

            await _next(context);
            context.Request.Body = originalRequestBody;
        }
    }
}
