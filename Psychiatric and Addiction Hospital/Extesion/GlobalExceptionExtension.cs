using Application.Common.Responses;
using System.Net;
using System.Text.Json;

namespace Psychiatric_and_Addiction_Hospital.Extesion
{
    public class GlobalExceptionExtension
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionExtension> _logger;

        public GlobalExceptionExtension(RequestDelegate next, ILogger<GlobalExceptionExtension> logger)
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
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }
        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception at {Path}", context.Request.Path);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var errors = new List<string> { ex.Message };
            if (ex.InnerException != null)
                errors.Add(ex.InnerException.Message);

            var response = ResponseFactory.Fail<string>(
                message: "Server Error",
                errors: new List<string> { ex.Message }
            );

            var json = JsonSerializer.Serialize(response);

            return context.Response.WriteAsync(json);
        }
    }
}
