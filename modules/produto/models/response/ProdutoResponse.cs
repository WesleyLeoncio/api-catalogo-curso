namespace api_catalogo_curso.modules.produto.models.response;

public record ProdutoResponse(
    int Id,
    string Nome,
    string Descricao,
    decimal Preco,
    string ImagemUrl,
    float Estoque,
    DateTime DataCadastro,
    int CategoriaId
);