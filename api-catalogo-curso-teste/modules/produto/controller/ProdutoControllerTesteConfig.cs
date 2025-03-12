using api_catalogo_curso_teste.config;
using api_catalogo_curso_teste.modules.produto.models;
using api_catalogo_curso.modules.common.unit_of_work.interfaces;
using api_catalogo_curso.modules.produto.models.mapper;
using AutoMapper;
using api_catalogo_curso.modules.produto.models.entity;

namespace api_catalogo_curso_teste.modules.produto.controller;

public class ProdutoControllerTesteConfig : IAsyncLifetime
{
    public readonly IUnitOfWork Uof;
    public readonly IMapper Mapper;
    
    public ProdutoControllerTesteConfig()
    {
        Uof = DatabaseConfig.CreateInstanceUof();
        Mapper = AutoMapperConfig.Configure(new ProdutoMapper());
    }
    
    public async Task InitializeAsync()
    {
        await CreateProdutos();
    }
    
    public Task DisposeAsync() => Task.CompletedTask; 

    private async Task CreateProdutos()
    {
        IEnumerable<Produto> produtos = Produtos.GetList();
        foreach (var produto in produtos)
        { 
            Uof.ProdutoRepository.Create(produto);
        }
        await Uof.Commit();
    }
    
    
}
