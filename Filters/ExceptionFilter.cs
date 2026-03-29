using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using ProjetoFinalPos.Exceptions;

namespace ProjetoFinalPos.Filters;

public class ExceptionHandlerFilter : IExceptionFilter
{
    private readonly ILogger<ExceptionHandlerFilter> _logger;

    public ExceptionHandlerFilter(ILogger<ExceptionHandlerFilter> logger)
    {
        _logger = logger;
    }

    public void OnException(ExceptionContext context)
    {
        var exception = context.Exception;
        var response = new { message = exception.Message };
        int statusCode;

        _logger.LogError($"Exception: {exception.GetType().Name} - {exception.Message}");

        if (exception is NotFoundException)
        {
            statusCode = StatusCodes.Status404NotFound;
        } 
        else if (exception is UnauthorizedException)
        {
            statusCode = StatusCodes.Status401Unauthorized;
        }
        else if (exception is BusinessException)
        {
            statusCode = StatusCodes.Status400BadRequest;
        }
        else
        {
            statusCode = StatusCodes.Status500InternalServerError;
            response = new { message = "Erro interno do servidor. Por favor, tente novamente." };
        }

        context.HttpContext.Response.StatusCode = statusCode;
        context.Result = new JsonResult(response);
        context.ExceptionHandled = true;
    }
}
