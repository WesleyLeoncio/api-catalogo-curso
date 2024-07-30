using api_catalogo_curso.infra.exceptions.custom;
using api_catalogo_curso.modules.common.unit_of_work.interfaces;
using api_catalogo_curso.modules.produto.models.entity;
using api_catalogo_curso.modules.produto.models.request;
using api_catalogo_curso.modules.produto.models.response;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace api_catalogo_curso.modules.produto.controller;

//UTILIZANDO O PADRÃO DO CURSO SEM SERVICE
[ApiController]
[Route("[controller]")]
public class ProdutoController : ControllerBase
{
    private readonly IUnitOfWork _uof;
    private readonly IMapper _mapper;

    public ProdutoController(IUnitOfWork uof,IMapper mapper)
    {
        _uof = uof;
        _mapper = mapper;
    }

    
    [HttpGet("Filter/Preco/Pagination")]
    public ActionResult<IEnumerator<ProdutoResponse>> ListarProdutoComFiltro([FromQuery] ProdutoFiltroRequest filtroRequest)
    {
        var produtos = _uof.ProdutoRepository.GetAllFilterPageable(filtroRequest);
        
        var metadata = new
        {
            produtos.TotalCount, produtos.PageSize, produtos.CurrentPage,
            produtos.TotalPages, produtos.HasNext, produtos.HasPrevious
        };
        //Adicionando dados da paginação no header da requisição
        Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(metadata));

        return Ok(_mapper.Map<IEnumerable<ProdutoResponse>>(produtos));
    }

    [HttpGet("{id}")]
    public ActionResult<ProdutoResponse> BuscarProduto(int id)
    {
        Produto produto = CheckProd(id);
        return _mapper.Map<ProdutoResponse>(produto);
    }

    [HttpPost]
    public ActionResult<ProdutoResponse> CadastroDeProduto(ProdutoRequest request)
    {
        Produto newProduto = _uof.ProdutoRepository.Create(_mapper.Map<Produto>(request));
        _uof.Commit();
        return CreatedAtAction(nameof(BuscarProduto),new 
            { id = newProduto.Id }, _mapper.Map<ProdutoResponse>(newProduto));
    }
    
    [HttpPut("{id}")]
    public ActionResult<ProdutoResponse> AlterarProduto(int id, ProdutoRequest request)
    {
        Produto produto = CheckProd(id);
        _mapper.Map(request, produto);
        Produto update = _uof.ProdutoRepository.Update(produto);
        _uof.Commit();
        return _mapper.Map<ProdutoResponse>(update);
    }

    [HttpDelete("{id}")]
    public ActionResult<ProdutoResponse> DeletarProduto(int id)
    {
        Produto produto = CheckProd(id);
        _uof.ProdutoRepository.Delete(produto);
        _uof.Commit();
        return _mapper.Map<ProdutoResponse>(produto);
    }

    private Produto CheckProd(int id)
    {
        return _uof.ProdutoRepository.Get(p => p.Id == id)??
               throw new NotFoundException("Produto não encontrada!");
    }
    
}