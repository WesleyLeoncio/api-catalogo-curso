using api_catalogo_curso.modules.categoria.models.entity;
using api_catalogo_curso.modules.common.unit_of_work.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace api_catalogo_curso.modules.categoria.controller;

[ApiController]
[Route("[controller]")]
public class CategoriaController : ControllerBase
{
    private readonly IUnitOfWork _uof;

    public CategoriaController(IUnitOfWork uof)
    {
        _uof = uof;
    }
    
    [HttpPost]
    public IActionResult CadastroDeCategoria(Categoria categoria)
    {
        Categoria newCategoria = _uof.CategoriaRepository.Create(categoria);
        _uof.Commit();
        return CreatedAtAction(nameof(BuscarCategoria),
            new { id = newCategoria.Id }, newCategoria);
    }
    
    [HttpGet("{id}")]
    public IActionResult BuscarCategoria(int id)
    {
        Categoria? categoria = _uof.CategoriaRepository.Get(c => c.Id == id);
        return Ok(categoria);
    }

    [HttpGet]
    public IActionResult ListarCategorias(int skip = 0, int take = 10 )
    {
        return Ok(_uof.CategoriaRepository.GetAll(skip, take));
    }
    
    [HttpGet("Produtos")]
    public IActionResult ListarCategoriaComProdutos(int skip = 0, int take = 10 )
    {
        return Ok(_uof.CategoriaRepository.GetAllInclude(skip, take));
    }
}