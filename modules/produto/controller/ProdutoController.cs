using api_catalogo_curso.modules.produto.repository.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace api_catalogo_curso.modules.produto.controller;

[ApiController]
[Route("[controller]")]
public class ProdutoController : ControllerBase
{
    private readonly IProdutoRepository _repository;

    public ProdutoController(IProdutoRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public IActionResult ListarProdutos(int skip = 0, int take = 10 )
    {
        return Ok(_repository.GetAll(skip, take));
    }
}