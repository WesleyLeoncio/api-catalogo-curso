using System.Linq.Expressions;
using api_catalogo_curso_teste.config;
using api_catalogo_curso.infra.exceptions.custom;
using api_catalogo_curso.modules.common.unit_of_work.interfaces;
using api_catalogo_curso.modules.produto.controller;
using api_catalogo_curso.modules.produto.models.entity;
using api_catalogo_curso.modules.produto.models.mapper;
using api_catalogo_curso.modules.produto.models.response;
using api_catalogo_curso.modules.produto.repository.interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace api_catalogo_curso_teste.modules.produto.controller;

public class DeleteProdutoTestes
{
    private readonly ProdutoController _controller;
    private readonly Mock<IUnitOfWork> _mockUof;
    private readonly Mock<IProdutoRepository> _mockProdutoRepository;

    public DeleteProdutoTestes()
    {
        
        _mockUof = new Mock<IUnitOfWork>();
        _mockProdutoRepository = new Mock<IProdutoRepository>();
        var mapper = AutoMapperConfig.Configure(new ProdutoMapper());
        
        _controller = new ProdutoController(_mockUof.Object, mapper);
        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext()
        };
    }
    
    [Fact(DisplayName = "Deve retornar NoContent ao deletar um produto")]
    public async Task DeleteProdutoById_Return_NoContent()
    {
        // Arrange
        _mockUof.Setup(u => u.ProdutoRepository).Returns(_mockProdutoRepository.Object);
        _mockProdutoRepository.Setup(repo => repo.Delete(It.IsAny<Produto>())).Verifiable();
        _mockProdutoRepository.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Produto, bool>>>()))
            .ReturnsAsync(new Produto { Id = 1, Nome = "Hamburgue", Descricao = "Hamburgue saboroso", Preco = 25, ImagemUrl = "hb.png" });
        var prodId = 1;
        
        // Act
        ActionResult act = await _controller.DeletarProduto(prodId);
        
        // Assert
        act.Should().BeOfType<NoContentResult>()
            .Which.StatusCode.Should().Be(204);
    }
    
    [Fact(DisplayName = "Deve lançar NotFoundException ao buscar produto por ID inexistente")]
    public async Task DeleteProdutoById_Return_NotFoundException()
    {
        // Arrange
        _mockUof.Setup(u => u.ProdutoRepository).Returns(_mockProdutoRepository.Object);
        _mockProdutoRepository.Setup(repo => repo.Delete(It.IsAny<Produto>())).Verifiable();
        _mockProdutoRepository.Setup(repo => repo.GetAsync(It.Is<Expression<Func<Produto, bool>>>(expr => false)))  // Simula um predicado que nunca retorna verdadeiro
            .ReturnsAsync(null as Produto);
        var prodId = 1;
        
        // Act
        Func<Task> act = async () => await _controller.DeletarProduto(prodId);
        
        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}