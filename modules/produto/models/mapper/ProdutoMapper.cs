using api_catalogo_curso.modules.common.pagination;
using api_catalogo_curso.modules.common.pagination.models.response;
using api_catalogo_curso.modules.produto.models.entity;
using api_catalogo_curso.modules.produto.models.request;
using api_catalogo_curso.modules.produto.models.response;
using AutoMapper;

namespace api_catalogo_curso.modules.produto.models.mapper;

public class ProdutoMapper : Profile
{
    public ProdutoMapper()
    {
        CreateMap<ProdutoRequest, Produto>();
        CreateMap<Produto, ProdutoResponse>();
        CreateMap<Produto, ProdutoCategoriaResponse>();
        CreateMap<Pageable<Produto>, PageableResponse<ProdutoResponse>>();
    }
}