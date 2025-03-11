using api_catalogo_curso.modules.produto.models.response;

namespace api_catalogo_curso.modules.categoria.models.response;

public record CategoriaProdutoResponse(
    int Id,
    string Nome,
    string ImagemUrl,
    IEnumerable<ProdutoCategoriaResponse> Produtos
);