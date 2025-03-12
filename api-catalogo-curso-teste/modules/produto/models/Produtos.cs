using api_catalogo_curso.modules.produto.models.entity;

namespace api_catalogo_curso_teste.modules.produto.models;

public static class Produtos
{
    public static IEnumerable<Produto> GetList()
    {
        return new List<Produto>
        {   new Produto
            {
                Id = 1,
                Nome = "Hamburgue",
                Descricao = "Hamburgue saboroso",
                Preco = 10.0m,
                Estoque = 50,
                ImagemUrl = "hb.png",
                CategoriaId = 2
            },
            new Produto
            {
                Id = 2,
                Nome = "Coca-Cola",
                Descricao = "Coca geladinha",
                Preco = 5,
                Estoque = 10,
                ImagemUrl = "cc.png",
                CategoriaId = 1
            }
        };
    }
    
}