using System.Net;
using System.Text.Json;

namespace DDDinner.Api.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred.");
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var statusCode = exception switch
            {
                ArgumentException => (int)HttpStatusCode.BadRequest,
                UnauthorizedAccessException => (int)HttpStatusCode.Unauthorized,
                _ => (int)HttpStatusCode.InternalServerError
            };

            var problemDetails = new
            {
                Type = statusCode switch
                {
                    (int)HttpStatusCode.BadRequest => "https://httpstatuses.com/400",
                    (int)HttpStatusCode.Unauthorized => "https://httpstatuses.com/401",
                    _ => "https://httpstatuses.com/500"
                },
                Title = statusCode switch
                {
                    (int)HttpStatusCode.BadRequest => "Bad Request",
                    (int)HttpStatusCode.Unauthorized => "Unauthorized",
                    _ => "Internal Server Error"
                },
                Status = statusCode,
                Detail = exception.Message,
                Instance = context.Request.Path
            };

            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = statusCode;

            var jsonResponse = JsonSerializer.Serialize(problemDetails);
            return context.Response.WriteAsync(jsonResponse);
        }
    }
}
