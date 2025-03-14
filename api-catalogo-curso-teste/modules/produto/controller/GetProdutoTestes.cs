using api_catalogo_curso_teste.modules.produto.models;
using api_catalogo_curso.infra.exceptions.custom;
using api_catalogo_curso.modules.produto.controller;
using api_catalogo_curso.modules.produto.models.enums;
using api_catalogo_curso.modules.produto.models.request;
using api_catalogo_curso.modules.produto.models.response;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace api_catalogo_curso_teste.modules.produto.controller;

public class GetProdutoTestes : IClassFixture<ProdutoControllerTesteConfig>
{
    private readonly ProdutoController _controller;

    public GetProdutoTestes(ProdutoControllerTesteConfig controller)
    {
        _controller = new ProdutoController(controller.Uof, controller.Mapper);
        _controller.ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() };
    }
    
    [Fact(DisplayName = "Deve retornar OkResult ao buscar produto por ID")]
    public async Task GetProdutoById_Return_OKResult()
    {
        // Arrange
        var prodId = 1;
        
        // Act
        ActionResult<ProdutoResponse> act = await _controller.BuscarProduto(prodId);
        
        // Assert
        act.Result.Should().BeOfType<OkObjectResult>()
            .Which.StatusCode.Should().Be(200);
    }
    
    [Fact(DisplayName = "Deve lançar NotFoundException ao buscar produto por ID inexistente")]
    public async Task GetProdutoById_Throws_NotFoundException()
    {
        // Arrange
        var prodId = 3; 
        
        // Act
        Func<Task> act = async () => await _controller.BuscarProduto(prodId);
        
        // Assert
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage("Produto não encontrado!");
    }
    
    [Fact(DisplayName = "Deve retornar um objeto do tipo ProdutoResponse")]
    public async Task GetProdutoById_Return_Type_ProdutoResponse()
    {
        // Arrange
        var prodId = 1; 
        
        // Act
        ActionResult<ProdutoResponse> act = await _controller.BuscarProduto(prodId);
        
        // Assert
        act.Result.Should().BeOfType<OkObjectResult>()
            .Which.Value.Should().BeOfType<ProdutoResponse>();
        
    }

   
    [Theory(DisplayName = "Deve testar o filtro de produtos")]
    [MemberData(nameof(ProdutosData.FiltroDeProdutosTestData), MemberType = typeof(ProdutosData))]
    public async Task GetAllProdutoComFiltros_Testa_Filtro(Criterio criterio, decimal preco,int expectedCount)
    {
        // Arrange
        ProdutoFiltroRequest request = new ProdutoFiltroRequest
        {
            Preco = preco,
            PrecoCriterio = criterio
        };
        
        // Act
        ActionResult<IEnumerable<ProdutoResponse>> act = await _controller.ListarProdutoComFiltro(request);
        var data = (act.Result as OkObjectResult)?.Value as IEnumerable<ProdutoResponse>;
        
        // Assert
        data.Should().HaveCount(expectedCount);
        
    }
    
    
}