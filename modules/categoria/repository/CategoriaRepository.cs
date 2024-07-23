using api_catalogo_curso.infra.data;
using api_catalogo_curso.modules.categoria.models.entity;
using api_catalogo_curso.modules.categoria.models.request;
using api_catalogo_curso.modules.categoria.repository.interfaces;
using api_catalogo_curso.modules.common.pagination;
using api_catalogo_curso.modules.common.pagination.models.request;
using api_catalogo_curso.modules.common.repository;
using Microsoft.EntityFrameworkCore;

namespace api_catalogo_curso.modules.categoria.repository;

public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
{
    public CategoriaRepository(AppDbConnectionContext context) : base(context) { }
    
    public Pageable<Categoria> GetAllIncludePageable(QueryParameters queryParameters)
    {
        IQueryable<Categoria> categorias = 
            GetIQueryable().OrderBy(c => c.Nome)
                .Include(categoria => categoria.Produtos);
        return Pageable<Categoria>
            .ToPagedList(categorias, queryParameters.PageNumber, queryParameters.PageSize);
    }

    public Pageable<Categoria> GetAllFilterPageable(CategoriaFiltroRequest filtroRequest)
    {
        IQueryable<Categoria> categorias = GetIQueryable();
        if (!string.IsNullOrEmpty(filtroRequest.Nome))
        {
            categorias = categorias.Where(c => 
                c.Nome != null && c.Nome.Contains(filtroRequest.Nome));
        }
        return Pageable<Categoria>
            .ToPagedList(categorias, filtroRequest.PageNumber, filtroRequest.PageSize);
    }
}