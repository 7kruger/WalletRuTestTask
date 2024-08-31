using System.Net;
using WalletRu.Application.Common.Result;
using FluentValidation;
using ILogger = Serilog.ILogger;

namespace WalletRu.Api.Middlewares;

public class CustomExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public CustomExceptionHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, ILogger logger)
    {
        try
        {
            await _next.Invoke(context);
        }
        catch (Exception exception)
        {
            logger.Error("Error: {exception}", exception);
            await HandleExceptionAsync(context, exception);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var code = HttpStatusCode.InternalServerError;
        Result result;
        
        switch(exception)
        {
            case ValidationException validationException:
                code = HttpStatusCode.BadRequest;
                result = Result.Failure(validationException.Errors.Select(x => x.ErrorMessage).ToList());
                break;
            default:
                result = Result.Failure(["Internal Server Error"]);
                break;
        }
        
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;
        
        return context.Response.WriteAsJsonAsync(result);
    }
}