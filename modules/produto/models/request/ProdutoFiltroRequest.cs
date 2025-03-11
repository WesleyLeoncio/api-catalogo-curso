using System.ComponentModel.DataAnnotations;
using api_catalogo_curso.modules.common.pagination.models.request;
using api_catalogo_curso.modules.produto.models.enums;

namespace api_catalogo_curso.modules.produto.models.request;

public class ProdutoFiltroRequest : QueryParameters
{
    public decimal? Preco { get; set; }
    
    [EnumDataType(typeof(Criterio))]
    public Criterio PrecoCriterio { get; set; }

    public bool VerificarValores()
    {
        return Preco.HasValue && Enum.IsDefined(PrecoCriterio);
    }
}