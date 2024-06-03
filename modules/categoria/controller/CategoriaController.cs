using api_catalogo_curso.modules.categoria.repository.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace api_catalogo_curso.modules.categoria.controller;

[ApiController]
[Route("[controller]")]
public class CategoriaController : ControllerBase
{
    private ICategoriaRepository _repository;

    public CategoriaController(ICategoriaRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public IActionResult ListarCategoria(int skip = 0, int take = 10 )
    {
        return Ok(_repository.GetAll(skip, take));
    }
}