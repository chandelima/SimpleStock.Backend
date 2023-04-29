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
        catch (System.Exception ex)
        { 
            await HandleExceptionAsync(context, ex);
        }
        
    }

    private static Task HandleExceptionAsync(HttpContext context, System.Exception exception)
    {
        var exceptionType = exception.GetType();
        if (!exceptionType.IsSubclassOf(typeof(BaseException)))
            throw exception;
        
        HttpStatusCode status = HttpStatusCode.BadRequest;
        string message = exception.Message;

        var result = JsonSerializer.Serialize(new { status, message });

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)status;

        return context.Response.WriteAsync(result);
    }
}
