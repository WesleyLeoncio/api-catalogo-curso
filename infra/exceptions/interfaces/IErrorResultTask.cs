namespace api_catalogo_curso.infra.exceptions.interfaces;

public interface IErrorResultTask
{
    public Task? ValidarException(ErrorExceptionResult error);
}