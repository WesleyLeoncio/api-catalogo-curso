using api_catalogo_curso.infra.exceptions.custom;
using api_catalogo_curso.modules.categoria.models.entity;
using api_catalogo_curso.modules.categoria.models.request;
using api_catalogo_curso.modules.categoria.models.response;
using api_catalogo_curso.modules.common.pagination.models.request;
using api_catalogo_curso.modules.common.unit_of_work.interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using X.PagedList;

namespace api_catalogo_curso.modules.categoria.controller;

[ApiController]
[Route("[controller]")]
public class CategoriaController : ControllerBase
{
    private readonly IUnitOfWork _uof;
    private readonly IMapper _mapper;

    public CategoriaController(IUnitOfWork uof, IMapper mapper)
    {
        _uof = uof;
        _mapper = mapper;
    }
    
    [HttpPost]
    public async Task<ActionResult<CategoriaResponse>> CadastroDeCategoria(CategoriaRequest request)
    {
        Categoria newCategoria = 
            _uof.CategoriaRepository.Create(_mapper.Map<Categoria>(request));
        await _uof.Commit();
        return CreatedAtAction(nameof(BuscarCategoria),new 
            { id = newCategoria.Id }, _mapper.Map<CategoriaResponse>(newCategoria));
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<CategoriaResponse>> BuscarCategoria(int id)
    {
        Categoria categoria = await CheckCategoria(id);
        return Ok(_mapper.Map<CategoriaResponse>(categoria));
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoriaResponse>>> ListarCategorias()
    {
        IEnumerable<Categoria> categorias = await _uof.CategoriaRepository.GetAllAsync();
        return Ok(_mapper.Map<IEnumerable<CategoriaResponse>>(categorias));
    }

    [HttpGet("Produtos/Pagination")]
    public async Task<ActionResult<IEnumerable<CategoriaProdutoResponse>>> ListarCategoriaComProdutos([FromQuery] QueryParameters queryParameters)
    {
        IPagedList<Categoria> categorias = 
            await _uof.CategoriaRepository.GetAllIncludePageableAsync(queryParameters);
        
        var metadata = new
        {
            categorias.Count, categorias.PageSize, categorias.PageCount,
            categorias.TotalItemCount, categorias.HasNextPage, categorias.HasPreviousPage
        };
        Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(metadata));
        return Ok(_mapper.Map<IEnumerable<CategoriaProdutoResponse>>(categorias));
    }
    
    [HttpGet("Filter/Pagination")]
    public async Task<ActionResult<IEnumerable<CategoriaResponse>>> ListarCategoriaComFiltro([FromQuery] CategoriaFiltroRequest filtroRequest)
    {
        IPagedList<Categoria> categorias = 
            await _uof.CategoriaRepository.GetAllFilterPageableAsync(filtroRequest);
       
        var metadata = new
        {
            categorias.Count, categorias.PageSize, categorias.PageCount,
            categorias.TotalItemCount, categorias.HasNextPage, categorias.HasPreviousPage
        };
        
        Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(metadata));
        return Ok(_mapper.Map<IEnumerable<CategoriaResponse>>(categorias));
    }
    
    [HttpPut("{id}")]
    public async Task<ActionResult<CategoriaResponse>> AlterarCategoria(int id,  CategoriaRequest request)
    {
        Categoria categoria = await CheckCategoria(id);
        _mapper.Map(request, categoria);
        Categoria update = _uof.CategoriaRepository.Update(categoria);
        await _uof.Commit();
        return Ok(_mapper.Map<CategoriaResponse>(update));
    }
    
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