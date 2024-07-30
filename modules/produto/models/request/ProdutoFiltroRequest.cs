using api_catalogo_curso.modules.common.pagination.models.request;

namespace api_catalogo_curso.modules.produto.models.request;

public class ProdutoFiltroRequest : QueryParameters
{
    public decimal? Preco { get; set; }
    public string? PrecoCriterio { get; set; }

    public bool VerificarValores()
    {
        return Preco.HasValue && !string.IsNullOrEmpty(PrecoCriterio);
    }
}