using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MrsCleanCapstone.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class MaintenanceMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        private readonly MaintenanceWindow _window;

        public MaintenanceMiddleware(RequestDelegate next, MaintenanceWindow window, ILogger<MaintenanceMiddleware> logger)
        {
            this._next = next;
            this._logger = logger;
            this._window = window;
        }
        public async Task Invoke(HttpContext httpContext)
        {
            if (_window.Enabled)
            {
                // set the code to 503 for SEO reasons
                httpContext.Response.StatusCode = (int)HttpStatusCode.ServiceUnavailable;
                httpContext.Response.Headers.Add("Retry-After", _window.RetryAfterInSeconds.ToString());
                httpContext.Response.ContentType = _window.ContentType;
                await httpContext
                    .Response
                    .WriteAsync(Encoding.UTF8.GetString(_window.Response), Encoding.UTF8);
            }
            await _next.Invoke(httpContext);
        }
    }
    public class MaintenanceWindow
    {

        private Func<bool> enabledFunc;
        private byte[] response;

        public MaintenanceWindow(Func<bool> enabledFunc, byte[] response)
        {
            this.enabledFunc = enabledFunc;
            this.response = response;
        }

        public bool Enabled => enabledFunc();
        public byte[] Response => response;

        public int RetryAfterInSeconds { get; set; } = 3600;
        public string ContentType { get; set; } = "text/html";
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class MaintenanceMiddlewareExtensions
    {
        public static IServiceCollection AddMaintenance(this IServiceCollection services, MaintenanceWindow window)
        {
            services.AddSingleton(window);
            return services;
        }

        public static IServiceCollection AddMaintenance(this IServiceCollection services, Func<bool> enabler, byte[] response, string contentType = "text/html", int retryAfterInSeconds = 3600)
        {
            AddMaintenance(services, new MaintenanceWindow(enabler, response)
            {
                ContentType = contentType,
                RetryAfterInSeconds = retryAfterInSeconds
            });

            return services;
        }
        public static IApplicationBuilder UseMaintenanceMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MaintenanceMiddleware>();
        }
    }
}
