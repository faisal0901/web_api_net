using System.Net;
using TokonyadiaRestAPI.DTO;
using TokonyadiaRestAPI.Exception;
using TokonyadiaRestAPI.Exceptions;

namespace TokonyadiaRestAPI.Middleware;

public class ExceptionHandlingMiddleware:IMiddleware
{
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;
    public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger)
    {
        _logger = logger;
    }
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);

        }
        catch (NotFoundException e)
        {
            context.Response.StatusCode = 404;
            await context.Response.WriteAsync("not found");
            _logger.LogError(e.Message);
        }
        catch (System.Exception e)
        {
            context.Response.StatusCode = 500;
            await context.Response.WriteAsync("internal server error");
            _logger.LogError(e.Message);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, System.Exception exception)
    {
        context.Response.ContentType = "application/json";

        var ErrorRespone = new ErrorResponse();
        switch (exception)
        {
            case NotFoundException:
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                ErrorRespone.StatusCode = (int)HttpStatusCode.NotFound;
                ErrorRespone.Message= exception.Message;
                break;
            case UnathorizedException:
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                ErrorRespone.StatusCode = (int)HttpStatusCode.Unauthorized;
                ErrorRespone.Message= exception.Message;
                break;
            case not null:
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                ErrorRespone.StatusCode = (int)HttpStatusCode.InternalServerError;
                ErrorRespone.Message="internal server error";
                break;
        }

        await context.Response.WriteAsJsonAsync(ErrorRespone);

    }

}