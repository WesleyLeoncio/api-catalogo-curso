using System.Linq.Expressions;
using api_catalogo_curso_teste.config;
using api_catalogo_curso_teste.modules.produto.models;
using api_catalogo_curso.infra.exceptions.custom;
using api_catalogo_curso.modules.common.unit_of_work.interfaces;
using api_catalogo_curso.modules.produto.controller;
using api_catalogo_curso.modules.produto.models.entity;
using api_catalogo_curso.modules.produto.models.mapper;
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

    public DeleteProdutoTestes()
    {
        _mockUof = new Mock<IUnitOfWork>();
        Mock<IProdutoRepository> mockProdutoRepository = new Mock<IProdutoRepository>();
        var mapper = AutoMapperConfig.Configure(new ProdutoMapper());

        _controller = new ProdutoController(_mockUof.Object, mapper);
        _controller.ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() };
        _mockUof.Setup(u => u.ProdutoRepository).Returns(mockProdutoRepository.Object);
    }

    [Fact(DisplayName = "Deve retornar NoContent ao deletar um produto")]
    public async Task DeleteProdutoById_Return_NoContent()
    {
        // Arrange
        _mockUof.Setup(u => u.ProdutoRepository.Delete(It.IsAny<Produto>())).Verifiable();
        _mockUof.Setup(u => u.ProdutoRepository.GetAsync(It.IsAny<Expression<Func<Produto, bool>>>()))
            .ReturnsAsync(ProdutosData.GetProdutoIndex(0));
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
        _mockUof.Setup(u => u.ProdutoRepository.Delete(It.IsAny<Produto>())).Verifiable();
        _mockUof.Setup(u => u.ProdutoRepository.GetAsync(It.Is<Expression<Func<Produto, bool>>>(expr => false))) // Simula um predicado que nunca retorna verdadeiro
            .ReturnsAsync(null as Produto);
      
        var prodId = 1;

        // Act
        Func<Task> act = async () => await _controller.DeletarProduto(prodId);

        // Assert
        await act.Should().ThrowAsync<NotFoundException>();
    }
}