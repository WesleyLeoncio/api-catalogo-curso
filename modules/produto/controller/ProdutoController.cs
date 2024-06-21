using api_catalogo_curso.modules.common.unit_of_work.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace api_catalogo_curso.modules.produto.controller;

[ApiController]
[Route("[controller]")]
public class ProdutoController : ControllerBase
{
    private readonly IUnitOfWork _uof;

    public ProdutoController(IUnitOfWork uof)
    {
        _uof = uof;
    }

    [HttpGet]
    public IActionResult ListarProdutos(int skip = 0, int take = 10 )
    {
        return Ok(_uof.ProdutoRepository.GetAll(skip, take));
    }
}