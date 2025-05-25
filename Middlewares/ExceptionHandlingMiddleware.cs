using MVCaptcha.Exceptions;
using MVCaptcha.Models.Errors;
using System.Text.Json;

namespace MVCaptcha.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionHandlingMiddleware(
        RequestDelegate next,
        ILogger<ExceptionHandlingMiddleware> logger,
        IHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ApiException ex)
            {
                _logger.LogError(ex, ex.Message);
                await HandleExceptionAsync(context, ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred");
                var apiException = new ApiException("An unexpected error occurred", StatusCodes.Status500InternalServerError, ex);
                await HandleExceptionAsync(context, apiException);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, ApiException exception)
        {
            //context.Response.ContentType = "application/json";
            //var response = context.Response;

            //var errorResponse = new ErrorResponse
            //{
            //    Success = false,
            //    Message = exception.Message
            //};

            //if (exception is ValidationException validationEx)
            //{
            //    response.StatusCode = validationEx.StatusCode;
            //}
            //else if (exception is NotFoundException)
            //{
            //    response.StatusCode = StatusCodes.Status404NotFound;
            //    errorResponse.Message = "Resource not found";
            //}
            //else if (exception is BadRequestException badRequestEx)
            //{
            //    response.StatusCode = badRequestEx.StatusCode;
            //    errorResponse.Message = badRequestEx.Message;
            //}
            //else
            //{
            //    response.StatusCode = exception.StatusCode;
            //    if (_env.IsDevelopment())
            //    {
            //        errorResponse.StackTrace = exception.StackTrace;
            //    }
            //}
            //await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));

            context.Response.ContentType = "text/html";
            context.Response.StatusCode = exception.StatusCode;

            var message = exception switch
            {
                NotFoundException => "Resource not found",
                ValidationException => "Validation error occurred",
                BadRequestException => exception.Message,
                _ => "An unexpected error occurred"
            };

            var stackTrace = _env.IsDevelopment() ? $"<pre>{exception.StackTrace}</pre>" : "";

            var html = $@"
        <html>
            <head><title>Error</title></head>
            <body style='font-family:sans-serif;padding:20px;'>
                <h1>Error</h1>
                <p><strong>Status Code:</strong> nigers</p>
                <p><strong>Message:</strong> {message}</p>
                {stackTrace}
            </body>
        </html>";

            await context.Response.WriteAsync(html);
        }

    }
}
