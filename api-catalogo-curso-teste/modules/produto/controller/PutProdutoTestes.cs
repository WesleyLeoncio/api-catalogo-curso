using System.Linq.Expressions;
using api_catalogo_curso_teste.config;
using api_catalogo_curso.infra.exceptions.custom;
using api_catalogo_curso.modules.common.unit_of_work.interfaces;
using api_catalogo_curso.modules.produto.controller;
using api_catalogo_curso.modules.produto.models.entity;
using api_catalogo_curso.modules.produto.models.mapper;
using api_catalogo_curso.modules.produto.models.request;
using api_catalogo_curso.modules.produto.models.response;
using api_catalogo_curso.modules.produto.repository.interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace api_catalogo_curso_teste.modules.produto.controller;

public class PutProdutoTestes
{
    private readonly ProdutoController _controller;
    private readonly Mock<IUnitOfWork> _mockUof;
    private readonly Mock<IProdutoRepository> _mockProdutoRepository;

    public PutProdutoTestes()
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

    [Fact(DisplayName = "Deve testar se o metodo de alterar produto retorn um OkResult")]
    public async Task PutProduto_Return_OkResult()
    {
        //Arrange
        _mockUof.Setup(u => u.ProdutoRepository).Returns(_mockProdutoRepository.Object);
        _mockProdutoRepository.Setup(repo => repo.Update(It.IsAny<Produto>()));
        _mockUof.Setup(u => u.Commit()).Returns(Task.CompletedTask);
        _mockProdutoRepository.Setup(repo => repo.GetAsync(It.IsAny<Expression<Func<Produto, bool>>>()))
            .ReturnsAsync(new Produto { Id = 1, Nome = "Hamburgue", Descricao = "Hamburgue saboroso", Preco = 25, ImagemUrl = "hb.png" });
       
        int id = 1;
        ProdutoRequest produtoRequest = new ProdutoRequest("Hamburgue", "Hamburgue saboroso",
            25, "hb.png", 40, 2);
    
        //Act
        ActionResult<ProdutoResponse> act = await _controller.AlterarProduto(id,produtoRequest);
    
        //Assert
        act.Should().NotBeNull();
        act.Result.Should().BeOfType<OkObjectResult>();
    }
    
    [Fact(DisplayName = "Deve testar se o metodo de alterar produto retorn um NotFoundException")]
    public async Task PutProduto_Return_NotFoundException()
    {
        //Arrange
        _mockUof.Setup(u => u.ProdutoRepository).Returns(_mockProdutoRepository.Object);
        _mockProdutoRepository.Setup(repo => repo.Update(It.IsAny<Produto>()));
        _mockProdutoRepository.Setup(repo => repo.GetAsync(It.Is<Expression<Func<Produto, bool>>>(expr => false)))  // Simula um predicado que nunca retorna verdadeiro
            .ReturnsAsync(null as Produto);
        _mockUof.Setup(u => u.Commit()).Returns(Task.CompletedTask);
        
        int idInexistente = 50;
        ProdutoRequest produtoRequest = new ProdutoRequest("Hamburgue", "Hamburgue saboroso",
            25, "hb.png", 40, 2);
    
        // Act
        Func<Task> act = async () => await _controller.AlterarProduto(idInexistente, produtoRequest);

        // Assert
        await act.Should()
            .ThrowAsync<NotFoundException>()
            .WithMessage("Produto não encontrado!");
    }
}