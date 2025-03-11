namespace api_catalogo_curso.modules.produto.models.response;

public record ProdutoCategoriaResponse(
    int Id,
    string Nome,
    string Descricao,
    decimal Preco,
    string ImagemUrl,
    float Estoque,
    DateTime DataCadastro
);