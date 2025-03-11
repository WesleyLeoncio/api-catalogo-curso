using api_catalogo_curso.infra.exceptions.custom;
using api_catalogo_curso.modules.categoria.models.entity;
using api_catalogo_curso.modules.categoria.models.request;
using api_catalogo_curso.modules.categoria.models.response;
using api_catalogo_curso.modules.common.pagination;
using api_catalogo_curso.modules.common.pagination.models.request;
using api_catalogo_curso.modules.common.unit_of_work.interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using X.PagedList;

namespace api_catalogo_curso.modules.categoria.controller;

[Route("[controller]")]
[ApiController]
public class CategoriaController : ControllerBase
{
    private readonly IUnitOfWork _uof;
    private readonly IMapper _mapper;

    public CategoriaController(IUnitOfWork uof, IMapper mapper)
    {
        _uof = uof;
        _mapper = mapper;
    }
    ///<summary>Cadastra Uma Nova Categoria</summary>
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Create))]
    [Authorize(policy: "ADMIN")]
    [HttpPost]
    public async Task<ActionResult<CategoriaResponse>> CadastroDeCategoria(CategoriaRequest request)
    {
        Categoria newCategoria = 
            _uof.CategoriaRepository.Create(_mapper.Map<Categoria>(request));
        await _uof.Commit();
        return CreatedAtAction(nameof(BuscarCategoria),new 
            { id = newCategoria.Id }, _mapper.Map<CategoriaResponse>(newCategoria));
    }
    
    ///<summary>Busca Uma Categoria Pelo Id</summary>
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
    [Authorize(policy: "USER")]
    [HttpGet("{id}")]
    public async Task<ActionResult<CategoriaResponse>> BuscarCategoria(int id)
    {
        Categoria categoria = await CheckCategoria(id);
        return Ok(_mapper.Map<CategoriaResponse>(categoria));
    }
    
    ///<summary>Lista Todas as Categorias</summary>
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
    [Authorize(policy: "USER")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoriaResponse>>> ListarCategorias()
    {
        IEnumerable<Categoria> categorias = await _uof.CategoriaRepository.GetAllAsync();
        return Ok(_mapper.Map<IEnumerable<CategoriaResponse>>(categorias));
    }
    
    ///<summary>Lista As Categorias Com Produtos e Filtro</summary>
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
    [Authorize(policy: "USER")]
    [HttpGet("Produtos/Pagination")]
    public async Task<ActionResult<IEnumerable<CategoriaProdutoResponse>>> ListarCategoriaComProdutos([FromQuery] QueryParameters queryParameters)
    {
        IPagedList<Categoria> categorias = 
            await _uof.CategoriaRepository.GetAllIncludePageableAsync(queryParameters);
        
        Response.Headers.Append("X-Pagination", 
            JsonConvert.SerializeObject(MetaData<Categoria>.ToValue(categorias)));
        return Ok(_mapper.Map<IEnumerable<CategoriaProdutoResponse>>(categorias));
    }
    
    ///<summary>Lista As Categorias Com Filtro</summary>
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
    [Authorize(policy: "USER")]
    [HttpGet("Filter/Pagination")]
    public async Task<ActionResult<IEnumerable<CategoriaResponse>>> ListarCategoriaComFiltro([FromQuery] CategoriaFiltroRequest filtroRequest)
    {
        IPagedList<Categoria> categorias = 
            await _uof.CategoriaRepository.GetAllFilterPageableAsync(filtroRequest);
        
        Response.Headers.Append("X-Pagination", 
            JsonConvert.SerializeObject(MetaData<Categoria>.ToValue(categorias)));
        return Ok(_mapper.Map<IEnumerable<CategoriaResponse>>(categorias));
    }
    
    /// <summary>Altera Uma Categoria</summary>
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
    [Authorize(policy: "ADMIN")]
    [HttpPut("{id}")]
    public async Task<ActionResult> AlterarCategoria(int id,  CategoriaRequest request)
    {
        Categoria categoria = await CheckCategoria(id);
        _mapper.Map(request, categoria);
        _uof.CategoriaRepository.Update(categoria);
        await _uof.Commit();
        return NoContent();
    }
    
    /// <summary>Deleta Uma Categoria</summary>
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Delete))]
    [Authorize(policy: "ADMIN")]
    [HttpDelete("{id}")]
    public async Task<ActionResult<CategoriaResponse>> DeletarCategoria(int id)
    { 
        Categoria categoria = await CheckCategoria(id);
        _uof.CategoriaRepository.Delete(categoria);
        await _uof.Commit();
        return Ok(_mapper.Map<CategoriaResponse>(categoria));
    }
    
    private async Task<Categoria> CheckCategoria(int id)
    {
        return await _uof.CategoriaRepository.GetAsync(c => c.Id == id)??
               throw new NotFoundException("Categoria não encontrada!");
    }
}