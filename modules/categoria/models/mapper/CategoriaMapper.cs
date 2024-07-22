using api_catalogo_curso.modules.categoria.models.entity;
using api_catalogo_curso.modules.categoria.models.request;
using api_catalogo_curso.modules.categoria.models.response;
using api_catalogo_curso.modules.common.models;
using AutoMapper;

namespace api_catalogo_curso.modules.categoria.models.mapper;

public class CategoriaMapper : Profile
{
    public CategoriaMapper()
    {
        CreateMap<CategoriaRequest, Categoria>();
        CreateMap<Categoria, CategoriaResponse>();
        CreateMap<Categoria, CategoriaProdutoResponse>();
    }
}