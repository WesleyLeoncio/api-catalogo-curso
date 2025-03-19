using api_catalogo_curso_teste.config;
using api_catalogo_curso_teste.modules.produto.models;
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

public class PostProdutoTestes 
{
    private readonly ProdutoController _controller;
    private readonly Mock<IUnitOfWork> _mockUof;

    public PostProdutoTestes()
    {
        _mockUof = new Mock<IUnitOfWork>();
        Mock<IProdutoRepository> mockProdutoRepository = new Mock<IProdutoRepository>();
        var mapper = AutoMapperConfig.Configure(new ProdutoMapper());
        
        _controller = new ProdutoController(_mockUof.Object, mapper);
        _controller.ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() };
        _mockUof.Setup(u => u.ProdutoRepository).Returns(mockProdutoRepository.Object);
    }

    [Theory(DisplayName = "Deve testar se o metodo cadastra o produto e retorna o codigo 201")]
    [MemberData(nameof(ProdutosData.ProdutosDataPostTeste), MemberType = typeof(ProdutosData))]
    public async Task PostProduto_Return_CreatedStatusCode(ProdutoRequest request)
    {
        //Arrange
        _mockUof.Setup(u => u.ProdutoRepository.Create(It.IsAny<Produto>()))
            .Returns((Produto p) => p);
        // Configura o Commit para não fazer nada (simulação)
        _mockUof.Setup(u => u.Commit()).Returns(Task.CompletedTask);

        //Act
        ActionResult<ProdutoResponse> act = await _controller.CadastroDeProduto(request);

        //Assert
        var createdResult =
            act.Result.Should().BeOfType<CreatedAtActionResult>();
        createdResult.Subject.StatusCode.Should().Be(201);
    }
    
    [Fact]
    public async Task PostProduto_Return_BadRequest_InvalidRequest()
    {
        // Arrange
        var request = new ProdutoRequest(
            Nome: null, // Campo obrigatório
            Descricao: null, // Campo obrigatório
            Preco: 0, // Fora do range
            ImagemUrl: null, // Campo obrigatório
            Estoque: 0, // Fora do range
            CategoriaId: 0 // Fora do range
        ); 
        
        // Adiciona erros ao ModelState para simular uma requisição inválida
        _controller.ModelState.AddModelError("Nome", "Campo Nome Obrigatorio!");
        _controller.ModelState.AddModelError("Preco", "O Preço deve está dentro do Range (1 a 20000)");
        // Act
        var result = await _controller.CadastroDeProduto(request);

        // Assert
        result.Result.Should().BeOfType<BadRequestObjectResult>()
            .Which.Value.Should().BeOfType<SerializableError>()
            .Which.Should().ContainKeys("Nome", "Preco"); // Verifica se os erros estão presentes
    }
    
}