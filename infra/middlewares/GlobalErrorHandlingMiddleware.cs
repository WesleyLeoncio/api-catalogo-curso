using api_catalogo_curso.infra.exceptions;
using api_catalogo_curso.infra.exceptions.interfaces;

namespace api_catalogo_curso.infra.middlewares;

public class GlobalErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;

    private readonly IEnumerable<IErrorResultTask> _validarException;
    
    public GlobalErrorHandlingMiddleware(RequestDelegate next, IEnumerable<IErrorResultTask> validarException)
    {
        _next = next;
        this._validarException = validarException;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex)!;
        }
    }

    private Task? HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        ErrorExceptionResult error = new ErrorExceptionResult(context, exception);
        foreach (var errorResultTask in _validarException)
        {
            Task erroResult = errorResultTask.ValidarException(error);
            if (erroResult != Task.FromResult(false)) return erroResult;
        }
      
        return error.GetResultPadrao(exception);
    }
    
 
}