namespace api_catalogo_curso.infra.exceptions.custom;

public class NotFoundException : Exception
{
    public NotFoundException(String msg) : base(msg){}
}