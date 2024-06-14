using System.Text.Json;

namespace api_catalogo_curso.infra.exceptions;

public class ErrorExceptionResult
{
    public HttpContext Context { get; set; }
    public string Mensage { get; set; }
    public Type ExceptionType { get; set; }
    

    public ErrorExceptionResult(HttpContext context, Exception exception)
    {
        Context = context;
        Mensage = exception.Message;
        ExceptionType = exception.GetType();
    }
    
    public Task? GetResultPadrao(Exception exception)
    {
        string msg = exception.Message;
        int status = 500;
        string result = JsonSerializer.Serialize(new { status , mensage = msg});
        Context.Response.StatusCode = status;
        return Context.Response.WriteAsync(result);
    }
}