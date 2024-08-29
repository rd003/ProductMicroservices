using ProductService.Exceptions;

namespace ProductService.Middlewares;

public class ExceptionMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger)
    {
        _logger = logger;
    }
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.Message);
            await HandleException(context, ex);
        }
    }

    private async Task HandleException(HttpContext context, Exception ex)
    {
        int statusCode = StatusCodes.Status500InternalServerError;

        switch (ex)
        {
            case NotFoundException _:
                statusCode = StatusCodes.Status404NotFound;
                break;
            case BadRequestException _:
                statusCode = StatusCodes.Status400BadRequest;
                break;
            default:
                break;
        }

        var errorResponse = new ErrorReponse
        {
            StatusCode = statusCode,
            Message = ex.Message
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;
        await context.Response.WriteAsync(errorResponse.ToString());
    }
}

