using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelApp.API.Middleware
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;

    public class RequestHotelMiddleware
    {
        private readonly RequestDelegate next;

        public RequestHotelMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context, ILogger logger)
        {
            var date = DateTime.Now;
            logger.LogInformation($"Handling request: {context.Request.Method} {context.Request.Path}");

            await this.next.Invoke(context);

            logger.LogInformation($"Finished handling request. Milliseconds: {(DateTime.Now - date).TotalMilliseconds}");
        }
    }
}
