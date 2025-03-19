using System.Linq.Expressions;
using api_catalogo_curso_teste.config;
using api_catalogo_curso_teste.modules.produto.models;
using api_catalogo_curso.infra.exceptions.custom;
using api_catalogo_curso.modules.common.unit_of_work.interfaces;
using api_catalogo_curso.modules.produto.controller;
using api_catalogo_curso.modules.produto.models.entity;
using api_catalogo_curso.modules.produto.models.enums;
using api_catalogo_curso.modules.produto.models.mapper;
using api_catalogo_curso.modules.produto.models.request;
using api_catalogo_curso.modules.produto.models.response;
using api_catalogo_curso.modules.produto.repository.interfaces;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using X.PagedList;

namespace api_catalogo_curso_teste.modules.produto.controller;

public class GetProdutoTestes 
{
    private readonly ProdutoController _controller;
    private readonly Mock<IUnitOfWork> _mockUof;


    public GetProdutoTestes()
    {
        _mockUof = new Mock<IUnitOfWork>();
        Mock<IProdutoRepository> mockProdutoRepository = new Mock<IProdutoRepository>();
        var mapper = AutoMapperConfig.Configure(new ProdutoMapper());
        
        _controller = new ProdutoController(_mockUof.Object, mapper);
        _controller.ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext() };
        _mockUof.Setup(u => u.ProdutoRepository).Returns(mockProdutoRepository.Object);
    }
    
    [Fact(DisplayName = "Deve retornar OkResult ao buscar produto por ID")]
    public async Task GetProdutoById_Return_OKResult()
    {
        // Arrange
        _mockUof.Setup(u => u.ProdutoRepository.GetAsync(It.IsAny<Expression<Func<Produto, bool>>>()))
            .ReturnsAsync(ProdutosData.GetProdutoIndex(0));
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
        // Simula um predicado que nunca retorna verdadeiro
        _mockUof.Setup(u => u.ProdutoRepository.GetAsync(It.Is<Expression<Func<Produto, bool>>>(expr => false))) 
            .ReturnsAsync(null as Produto);
        var prodId = 50; 
        
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
        _mockUof.Setup(u => u.ProdutoRepository.GetAsync(It.IsAny<Expression<Func<Produto, bool>>>()))
            .ReturnsAsync(ProdutosData.GetProdutoIndex(0));
        var prodId = 1; 
        
        // Act
        ActionResult<ProdutoResponse> act = await _controller.BuscarProduto(prodId);
        
        // Assert
        act.Result.Should().BeOfType<OkObjectResult>()
            .Which.Value.Should().BeOfType<ProdutoResponse>();
        
    }

    [Theory(DisplayName = "Deve testar o filtro de produtos")]
    [MemberData(nameof(ProdutosData.FiltroDeProdutosTestData), MemberType = typeof(ProdutosData))]
    public async Task GetAllProdutoComFiltros_Testa_Filtro(Criterio criterio, decimal preco, int expectedCount)
    {
        // Arrange
        _mockUof.Setup(u => u.ProdutoRepository.GetAllFilterPageableAsync(It.IsAny<ProdutoFiltroRequest>()))
            .ReturnsAsync((ProdutoFiltroRequest request) =>
            {
                var produtosFiltrados = ProdutosData.GetListProdutos()
                    .Where(p => request.PrecoCriterio switch
                    {
                        Criterio.MAIOR => p.Preco > request.Preco,
                        Criterio.MENOR => p.Preco < request.Preco,
                        Criterio.IGUAL => p.Preco == request.Preco,
                        _ => true // Caso padrão (sem filtro)
                    })
                    .ToList();

                return produtosFiltrados.ToPagedList(1, 10);
            });

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