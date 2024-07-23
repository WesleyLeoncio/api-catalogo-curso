using api_catalogo_curso.modules.categoria.models.request;
using api_catalogo_curso.modules.categoria.models.response;
using api_catalogo_curso.modules.categoria.service.interfaces;
using api_catalogo_curso.modules.common.pagination.models.request;
using api_catalogo_curso.modules.common.pagination.models.response;
using Microsoft.AspNetCore.Mvc;

namespace api_catalogo_curso.modules.categoria.controller;

[ApiController]
[Route("[controller]")]
public class CategoriaController : ControllerBase
{
    private readonly ICategoriaService _service;
    public CategoriaController(ICategoriaService service)
    {
        _service = service;
    }

    [HttpPost]
    public ActionResult<CategoriaResponse> CadastroDeCategoria(CategoriaRequest request)
    {
        CategoriaResponse response = _service.Create(request);
        
        return CreatedAtAction(nameof(BuscarCategoria),new { id = response.Id }, response);
    }
    
    [HttpGet("{id}")]
    public ActionResult<CategoriaResponse> BuscarCategoria(int id)
    {
        return Ok(_service.GetId(id));
    }
    
    [HttpGet]
    public ActionResult<CategoriaResponse> ListarCategorias()
    {
        return Ok(_service.GetAll());
    }

    [HttpGet("Produtos/Pagination")]
    public ActionResult<PageableResponse<CategoriaProdutoResponse>> ListarCategoriaComProdutos([FromQuery] QueryParameters queryParameters)
    {
        return Ok(_service.GetAllIncludePageable(queryParameters));
    }
    
    [HttpGet("Filter/Pagination")]
    public ActionResult<PageableResponse<CategoriaResponse>> ListarCategoriaComFiltro([FromQuery] CategoriaFiltroRequest filtroRequest)
    {
        return Ok(_service.GetAllFilterPageable(filtroRequest));
    }
    
    [HttpPut("{id}")]
    public ActionResult<CategoriaResponse> AlterarCategoria(int id,  CategoriaRequest request)
    {
        return _service.Update(id, request);
    }
    
    [HttpDelete("{id}")]
    public ActionResult<CategoriaResponse> DeletarCategoria(int id)
    { 
        return _service.Delete(id);
    }
    
    
}