using AcerPro.Presentation.Server.Infrastructures;
using FluentResults;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Text.Json;
using System.Threading.Tasks;

namespace AcerPro.Presentation.Server.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next,ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(UnauthorizedAccessException ex)
            {
                Debug.WriteLine($"The following error happened: {ex.Message}");
                _logger.LogError(ex,ex.Message);

                context.Response.StatusCode = 401;
                context.Response.ContentType = "application/json";
                var stream = context.Response.Body;
                await JsonSerializer.SerializeAsync(stream, Result.Fail(ex.Message).ToAPIResponse());
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"The following error happened: {ex.Message}");
                _logger.LogError(ex, ex.Message);

                context.Response.StatusCode = 500;
                context.Response.ContentType = "application/json";
                var stream = context.Response.Body;
                await JsonSerializer.SerializeAsync(stream, Result.Fail(ex.Message).ToAPIResponse());
            }
        }
    }
}
