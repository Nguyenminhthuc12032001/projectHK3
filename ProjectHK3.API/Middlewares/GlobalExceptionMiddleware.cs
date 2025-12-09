using ProjectHK3.Application.Exceptions;
using System.Diagnostics;
using System.Net;
using System.Text.Json;

namespace ProjectHK3.Api.Middlewares
{
    public class GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
    {
        readonly RequestDelegate _next = next;
        readonly ILogger<GlobalExceptionMiddleware> _logger = logger;
        private static readonly JsonSerializerOptions _jsonOptions = new() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

        public async Task InvokeAsync(HttpContext context)
        {
            string traceId = Activity.Current?.Id ?? context.TraceIdentifier;

            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                await HandleExceptionAsync(context, e, traceId);
            }
        }

        public async Task HandleExceptionAsync(HttpContext context, Exception e, string traceId)
        {
            context.Response.Clear();
            context.Response.ContentType = "application/problem+json";
            int statusCode = (int)HttpStatusCode.InternalServerError;
            string message = "An unexpected error occurred.";
            string errorType = "ServerError";

            switch (e)
            {
                case BusinessException be:
                    statusCode = be.StatusCode;
                    message = be.Message;
                    errorType = "BusinessError";
                    break;

                case UnauthorizedAccessException:
                    statusCode = (int)HttpStatusCode.Unauthorized;
                    message = "Unauthorized access.";
                    errorType = "unauthorized";
                    break;

                case KeyNotFoundException:
                    statusCode = (int)HttpStatusCode.NotFound;
                    message = "Resource not found.";
                    errorType = "NotFound";
                    break;

                case InvalidOperationException:
                    statusCode = (int)HttpStatusCode.BadRequest;
                    message = e.Message;
                    errorType = "InvalidOperation";
                    break;

                case OperationCanceledException:
                    statusCode = 499;
                    message = "The request was canceled by the client.";
                    errorType = "requestCanceled";
                    break;
                case FluentValidation.ValidationException ve:
                    statusCode = (int)HttpStatusCode.BadRequest;
                    message = ve.Message;
                    errorType = "ValidationError";
                    break;
                default:
                    statusCode = (int)HttpStatusCode.InternalServerError;
                    message = e.Message;
                    errorType = "ServerError";
                    break;

            }

            if (context.Response.HasStarted)
            {
                _logger.LogError(e,
                    "Response already started. TraceId={TraceId}, Path={Path}, Status={Status}, Type={Type}, Message={Message}",
                    traceId, context.Request.Path, statusCode, errorType, message);
                return;
            }

            context.Response.StatusCode = statusCode;

            _logger.LogError(e,
                "[Error] TraceId={TraceId}, Path={Path}, Status={Status}, Type={Type}, Message={Message}",
                traceId, context.Request.Path, statusCode, errorType, message);
            var payload = new
            {
                type = errorType,
                title = errorType switch
                {
                    "BusinessError" => "Business rule violated",
                    "unauthorized" => "Unauthorized",
                    "NotFound" => "Resource not found",
                    "ValidationError" => "Data validation failed",
                    _ => "Unexpected error"
                },
                status = statusCode,
                traceId,
                detail = message,
                instance = context.Request.Path
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(payload, _jsonOptions));
        }
    }
}
