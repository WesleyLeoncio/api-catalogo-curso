using api_catalogo_curso.modules.produto.models.entity;
using api_catalogo_curso.modules.produto.models.response;
using AutoMapper;

namespace api_catalogo_curso.modules.produto.models.mapper;

public class ProdutoMapper : Profile
{
    public ProdutoMapper()
    {
        CreateMap<Produto, ProdutoResponse>();
        CreateMap<Produto, ProdutoCategoriaResponse>();
    }
}