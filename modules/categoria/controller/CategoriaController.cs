using api_catalogo_curso.infra.exceptions.custom;
using api_catalogo_curso.modules.categoria.repository.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace api_catalogo_curso.modules.categoria.controller;

[ApiController]
[Route("[controller]")]
public class CategoriaController : ControllerBase
{
    private readonly ICategoriaRepository _repository;

    public CategoriaController(ICategoriaRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public IActionResult ListarCategorias(int skip = 0, int take = 10 )
    {
        throw new NotFoundException("NotFoundException");
        return Ok(_repository.GetAll(skip, take));
    }
    
    [HttpGet("Produtos")]
    public IActionResult ListarCategoriaComProdutos(int skip = 0, int take = 10 )
    {
        throw new Exception("Exception");
        return Ok(_repository.GetAllInclude(skip, take));
    }
}