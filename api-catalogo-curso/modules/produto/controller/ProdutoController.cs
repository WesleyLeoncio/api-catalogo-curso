﻿using api_catalogo_curso.infra.exceptions.custom;
using api_catalogo_curso.modules.common.pagination;
using api_catalogo_curso.modules.common.unit_of_work.interfaces;
using api_catalogo_curso.modules.produto.models.entity;
using api_catalogo_curso.modules.produto.models.request;
using api_catalogo_curso.modules.produto.models.response;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using X.PagedList;
using Microsoft.AspNetCore.Http;

namespace api_catalogo_curso.modules.produto.controller;

[ApiController]
[Route("[controller]")]
public class ProdutoController : ControllerBase
{
    private readonly IUnitOfWork _uof;
    private readonly IMapper _mapper;

    public ProdutoController(IUnitOfWork uof, IMapper mapper)
    {
        _uof = uof;
        _mapper = mapper;
    }

    ///<summary>Lista Produtos Com Filtro</summary>
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
    [Authorize(policy: "USER")]
    [HttpGet("Filter/Preco/Pagination")]
    public async Task<ActionResult<IEnumerable<ProdutoResponse>>> ListarProdutoComFiltro(
        [FromQuery] ProdutoFiltroRequest filtroRequest)
    {
        IPagedList<Produto> produtos = await _uof.ProdutoRepository.GetAllFilterPageableAsync(filtroRequest);

        //Adicionando dados da paginação no header da requisição
        Response.Headers.Append("X-Pagination",
            JsonConvert.SerializeObject(MetaData<Produto>.ToValue(produtos)));

        return Ok(_mapper.Map<IEnumerable<ProdutoResponse>>(produtos));
    }

    ///<summary>Busca Um Produto Pelo Id</summary>
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
    [Authorize(policy: "USER")]
    [HttpGet("{id}")]
    public async Task<ActionResult<ProdutoResponse>> BuscarProduto(int id)
    {
        Produto produto = await CheckProd(id);
        return Ok(_mapper.Map<ProdutoResponse>(produto));
    }

    ///<summary>Cadastra Um Novo Produto</summary>
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Create))]
    [Authorize(policy: "ADMIN")]
    [HttpPost]
    public async Task<ActionResult<ProdutoResponse>> CadastroDeProduto(ProdutoRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        Produto newProduto = _uof.ProdutoRepository.Create(_mapper.Map<Produto>(request));
        await _uof.Commit();
        return CreatedAtAction(nameof(BuscarProduto), new
            { id = newProduto.Id }, _mapper.Map<ProdutoResponse>(newProduto));
    }

    [Authorize(policy: "ADMIN")]
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> AlterarProduto(int id, ProdutoRequest request)
    {
        Produto produto = await CheckProd(id);
        _mapper.Map(request, produto);
        _uof.ProdutoRepository.Update(produto);
        await _uof.Commit();
        return Ok(new { mensagem = "Produto atualizado com sucesso!" });
    }

    [Authorize(policy: "ADMIN")]
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesDefaultResponseType]
    public async Task<ActionResult> DeletarProduto(int id)
    {
        Produto produto = await CheckProd(id);
        _uof.ProdutoRepository.Delete(produto);
        await _uof.Commit();
        return NoContent();
    }

    private async Task<Produto> CheckProd(int id)
    {
        return await _uof.ProdutoRepository.GetAsync(p => p.Id == id) ??
               throw new NotFoundException("Produto não encontrado!");
    }
}