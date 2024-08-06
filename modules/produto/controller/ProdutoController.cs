using api_catalogo_curso.infra.exceptions.custom;
using api_catalogo_curso.modules.common.pagination;
using api_catalogo_curso.modules.common.unit_of_work.interfaces;
using api_catalogo_curso.modules.produto.models.entity;
using api_catalogo_curso.modules.produto.models.request;
using api_catalogo_curso.modules.produto.models.response;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using X.PagedList;

namespace api_catalogo_curso.modules.produto.controller;


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
    public async Task<ActionResult<IEnumerator<ProdutoResponse>>> ListarProdutoComFiltro([FromQuery] ProdutoFiltroRequest filtroRequest)
    {
        IPagedList<Produto> produtos =  await _uof.ProdutoRepository.GetAllFilterPageableAsync(filtroRequest);
      
       
        //Adicionando dados da paginação no header da requisição
        Response.Headers.Append("X-Pagination", 
            JsonConvert.SerializeObject(MetaData<Produto>.ToValue(produtos)));

        return Ok(_mapper.Map<IEnumerable<ProdutoResponse>>(produtos));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProdutoResponse>> BuscarProduto(int id)
    {
        Produto produto = await CheckProd(id);
        return Ok(_mapper.Map<ProdutoResponse>(produto));
    }

    [HttpPost]
    public async Task<ActionResult<ProdutoResponse>> CadastroDeProduto(ProdutoRequest request)
    {
        Produto newProduto = _uof.ProdutoRepository.Create(_mapper.Map<Produto>(request));
        await _uof.Commit();
        return CreatedAtAction(nameof(BuscarProduto),new 
            { id = newProduto.Id }, _mapper.Map<ProdutoResponse>(newProduto));
    }
    
    [HttpPut("{id}")]
    public async Task<ActionResult<ProdutoResponse>> AlterarProduto(int id, ProdutoRequest request)
    {
        Produto produto =  await CheckProd(id);
        _mapper.Map(request, produto);
        Produto update = _uof.ProdutoRepository.Update(produto);
        await _uof.Commit();
        return Ok(_mapper.Map<ProdutoResponse>(update));
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult<ProdutoResponse>> DeletarProduto(int id)
    {
        Produto produto = await CheckProd(id);
        _uof.ProdutoRepository.Delete(produto);
        await _uof.Commit();
        return Ok(_mapper.Map<ProdutoResponse>(produto));
    }

    private async Task<Produto> CheckProd(int id)
    {
        return await _uof.ProdutoRepository.GetAsync(p => p.Id == id)??
               throw new NotFoundException("Produto não encontrado!");
    }
    
}