using System.Net;

namespace ApplicationName.Api.Middlewares;

internal class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext httpContext, ILogger<ExceptionMiddleware> logger)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Wrong request execution. {ErrorMessage}", e.Message);
            await WriteErrorResponse(httpContext, HttpStatusCode.InternalServerError, e.Message);
        }
    }

    private static async Task WriteErrorResponse(HttpContext httpContext, HttpStatusCode httpStatusCode, string message)
    {
        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = (int)httpStatusCode;
        await httpContext.Response.WriteAsJsonAsync(message);
    }
}