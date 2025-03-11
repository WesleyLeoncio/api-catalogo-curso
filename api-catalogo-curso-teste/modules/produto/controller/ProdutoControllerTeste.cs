using api_catalogo_curso.infra.data;
using api_catalogo_curso.modules.common.unit_of_work;
using api_catalogo_curso.modules.common.unit_of_work.interfaces;
using api_catalogo_curso.modules.produto.controller;
using api_catalogo_curso.modules.produto.models.mapper;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api_catalogo_curso.modules.produto.models.entity;

namespace api_catalogo_curso_teste.modules.produto.controller;

public class ProdutoControllerTeste
{
    private readonly ProdutoController _controller;
    private readonly IUnitOfWork _repository;
    private readonly IMapper _mapper;
    private readonly AppDbConnectionContext _context;

    private static DbContextOptions<AppDbConnectionContext> dbContextOptions { get; }

    static ProdutoControllerTeste()
    {
        // Configura o banco em memória
        dbContextOptions = new DbContextOptionsBuilder<AppDbConnectionContext>()
            .UseInMemoryDatabase("TestDatabase")
            .Options;
    }

    public ProdutoControllerTeste()
    {
        // Inicializa o contexto com o banco em memória
        _context = new AppDbConnectionContext(dbContextOptions);
        
        // Inicializa o repositório com o contexto em memória
        _repository = new UnitOfWork(_context);

        // Configura o AutoMapper
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new ProdutoMapper()); // Substitua com o perfil correto
        });
        _mapper = config.CreateMapper();

        // Inicializa o controller com as dependências
        _controller = new ProdutoController(_repository, _mapper);

        // Adiciona dados de teste no banco em memória
        Produto produto = new Produto
        {
            Id = 1,
            Nome = "Hamburgue",
            Descricao = "Hamburgue saboroso",
            Preco = 10.0m,
            Estoque = 50,
            ImagemUrl = "hb.png",
            CategoriaId = 2
        };
        _context.ProdutoBd?.Add(produto);
        _context.SaveChanges();
    }

    [Fact]
    public async Task GetProdutoById_Return_OKResult()
    {
        // Arrange
        var prodId = 1; // Produto inserido no banco em memória
        
        // Act
        var data = await _controller.BuscarProduto(prodId);
        
        // Verifica se o retorno foi OkObjectResult
        var okResult = Assert.IsType<OkObjectResult>(data.Result);
        
        // Assert
        // Verifica o status de resposta
        Assert.Equal(200, okResult.StatusCode);
        
    }
}
