using SimpleStock.Exception;
using System.Net;
using System.Text.Json;

namespace SimpleStock.API.Middlewares;

public class GlobalErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public GlobalErrorHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (BaseException ex)
        { 
            await HandleExceptionAsync(context, ex);
        }
        
    }

    private static Task HandleExceptionAsync(HttpContext context, System.Exception exception)
    {
        HttpStatusCode status = HttpStatusCode.BadRequest;
        string message = exception.Message;

        var result = JsonSerializer.Serialize(new { status, message });

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)status;

        return context.Response.WriteAsync(result);
    }
}
