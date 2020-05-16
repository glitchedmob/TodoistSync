using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Sentry;

namespace TodoistSync.Middleware
{
    public class SentryEventsFlusherMiddleware
    {
        private readonly RequestDelegate _next;

        public SentryEventsFlusherMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ISentryClient sentryClient)
        {
            context.Response.OnCompleted(async () =>
            {
                await sentryClient.FlushAsync(timeout: TimeSpan.FromSeconds(10));
            });

            await _next(context);
        }
    }
}
