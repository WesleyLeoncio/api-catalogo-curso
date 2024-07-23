using api_catalogo_curso.modules.common.pagination.models.request;

namespace api_catalogo_curso.modules.categoria.models.request;

public class CategoriaFiltroRequest : QueryParameters
{
    public string? Nome { get; set; }
}