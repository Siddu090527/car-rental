using System.Text.Json;

namespace CarRental.Api.Middleware;

/// <summary>
/// Handles unhandled exceptions and returns consistent HTTP responses.
/// </summary>
public sealed class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(
        RequestDelegate next,
        ILogger<ExceptionMiddleware> logger)
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
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning(ex, ex.Message);

            context.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;
            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(
                JsonSerializer.Serialize(new
                {
                    statusCode = StatusCodes.Status422UnprocessableEntity,
                    error = "Validation Failed",
                    message = ex.Message
                }));
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning(ex, ex.Message);

            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(
                JsonSerializer.Serialize(new
                {
                    statusCode = StatusCodes.Status400BadRequest,
                    error = "Bad Request",
                    message = ex.Message
                }));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(
                JsonSerializer.Serialize(new
                {
                    statusCode = StatusCodes.Status500InternalServerError,
                    error = "Internal Server Error",
                    message = "An unexpected error occurred."
                }));
        }
    }
}