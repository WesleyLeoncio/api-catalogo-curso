using System.Text.Json;
using api_catalogo_curso.infra.exceptions.custom;
using api_catalogo_curso.infra.exceptions.interfaces;

namespace api_catalogo_curso.infra.exceptions.handle;

public class HandleNotFound : IErrorResultTask
{
    public Task? ValidarException(ErrorExceptionResult error)
    {
        if (error.ExceptionType == typeof(NotFoundException))
        {
            int status = 404;
            string result = JsonSerializer.Serialize(new { status, mensage = error.Mensage});
            error.Context.Response.StatusCode = status;
            return error.Context.Response.WriteAsync(result);
        }
        
        return null;
    }
}