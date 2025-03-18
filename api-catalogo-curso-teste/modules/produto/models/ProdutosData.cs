using api_catalogo_curso.modules.produto.models.entity;
using api_catalogo_curso.modules.produto.models.enums;
using api_catalogo_curso.modules.produto.models.request;
using api_catalogo_curso.modules.produto.models.response;

namespace api_catalogo_curso_teste.modules.produto.models;

public static class ProdutosData
{
    public static IEnumerable<Produto> GetListProdutos()
    {
        return new List<Produto>
        {
            new Produto
            {
                Id = 1, Nome = "Hamburgue", Descricao = "Hamburgue saboroso",
                Preco = 22m, Estoque = 50, ImagemUrl = "hb.png", CategoriaId = 2
            },
            new Produto
            {
                Id = 2, Nome = "Coca-Cola", Descricao = "Coca geladinha",
                Preco = 10m, Estoque = 10, ImagemUrl = "cc.png", CategoriaId = 1
            },
            new Produto
            {
                Id = 3, Nome = "Pizza", Descricao = "Pizza Saborosa",
                Preco = 100m, Estoque = 10, ImagemUrl = "pz.png", CategoriaId = 1
            }
        };
    }


    // Propriedade estática pública que fornece os dados de teste
    public static IEnumerable<object[]> FiltroDeProdutosTestData =>
        new List<object[]>
        {
            // Teste de filtro MAIOR: Espera 1 produto com preço maior que 90
            new object[] { Criterio.MAIOR, 90, 1 },

            // Teste de filtro MENOR: Espera 2 produtos com preço menor que 25
            new object[] { Criterio.MENOR, 25, 2 },

            // Teste de filtro IGUAL: Espera 1 produto com preço igual a 10
            new object[] { Criterio.IGUAL, 10, 1 }
        };

    
    public static IEnumerable<object[]> ProdutosDataPostTeste()
    {
        return new List<object[]>
        {
            new object[]
            {
                new ProdutoRequest("Cachorro Quente", "Cachorro quente mais gostoso da cidade.", 15, "cq.png", 5, 2)
            },
            new object[] { new ProdutoRequest("Heineken", "Cerveja Geladinha.", 7, "cv.png", 20, 1) }
        };
    }
  
}