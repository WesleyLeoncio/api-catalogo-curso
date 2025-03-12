using api_catalogo_curso.infra.exceptions.custom;
using api_catalogo_curso.modules.produto.controller;
using api_catalogo_curso.modules.produto.models.response;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace api_catalogo_curso_teste.modules.produto.controller;

public class GetProdutoTestes : IClassFixture<ProdutoControllerTesteConfig>
{
    private readonly ProdutoController _controller;

    public GetProdutoTestes(ProdutoControllerTesteConfig controller)
    {
        _controller = new ProdutoController(controller.Uof, controller.Mapper);
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
    public async Task GetProdutoById_ReturnTypeProdutoResponse()
    {
        // Arrange
        var prodId = 1; 
        
        // Act
        ActionResult<ProdutoResponse> act = await _controller.BuscarProduto(prodId);
        
        // Assert
        act.Result.Should().BeOfType<OkObjectResult>()
            .Which.Value.Should().BeOfType<ProdutoResponse>();
        
    }
    
}