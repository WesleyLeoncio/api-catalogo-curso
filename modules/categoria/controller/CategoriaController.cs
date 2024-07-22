using api_catalogo_curso.modules.categoria.models.request;
using api_catalogo_curso.modules.categoria.models.response;
using api_catalogo_curso.modules.categoria.service.interfaces;
using api_catalogo_curso.modules.common.models;
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
    public ActionResult<CategoriaResponse> ListarCategorias(int skip = 0, int take = 10)
    {
        return Ok(_service.GetAll(skip, take));
    }

    [HttpGet("Produtos")]
    public ActionResult<CategoriaProdutoResponse> ListarCategoriaComProdutos([FromQuery] QueryParameters queryParameters)
    {
        return Ok(_service.GetAllInclude(queryParameters));
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