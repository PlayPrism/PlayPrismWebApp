using System.Net;
using PlayPrism.Contracts.V1.Responses.Api;

namespace PlayPrism.API.Extensions;

/// <summary>
/// Represents exception middleware
/// </summary>
public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="ExceptionMiddleware"/> class.
    /// </summary>
    /// <param name="next">Invoke next middleware</param>
    /// <param name="logger">Logger for middleware</param>
    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _logger = logger;
        _next = next;
    }

    /// <summary>
    /// Define whether any errors in <see cref="HttpContext"/>. If errors exist call <see cref="HandleExceptionAsync"/>
    /// </summary>
    /// <param name="httpContext"><see cref="HttpContext"/></param>
    /// <returns>A <see cref="Task"/> representing the result of the asynchronous operation.</returns>
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Something went wrong: {ex}");
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        await context.Response.WriteAsync(new ErrorDetails()
        {
            StatusCode = context.Response.StatusCode,
            Message = "Internal Server Error."
        }.ToString());
    }
}

/// <summary>
/// Represents extension for exception middleware
/// </summary>
public static class ExceptionMiddlewareExtension
{
    /// <summary>
    /// Add <see cref="ExceptionMiddleware"/> to service collection
    /// </summary>
    /// <param name="app"><see cref="IApplicationBuilder"/></param>
    public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionMiddleware>();
    }
}