using api_catalogo_curso.infra.data;
using api_catalogo_curso.modules.categoria.models.entity;
using api_catalogo_curso.modules.categoria.models.request;
using api_catalogo_curso.modules.categoria.repository.interfaces;
using api_catalogo_curso.modules.common.pagination.models.request;
using api_catalogo_curso.modules.common.repository;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace api_catalogo_curso.modules.categoria.repository;

public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
{
    public CategoriaRepository(AppDbConnectionContext context) : base(context)
    {
    }

    public async Task<IPagedList<Categoria>> GetAllIncludePageableAsync(QueryParameters queryParameters)
    {
        
        IEnumerable<Categoria> categorias = await GetIQueryable()
            .OrderBy(c => c.Nome)
            .Include(categoria => categoria.Produtos)
            .ToListAsync();
        return await categorias.ToPagedListAsync(queryParameters.PageNumber,
            queryParameters.PageSize);
    }

    public async Task<IPagedList<Categoria>> GetAllFilterPageableAsync(CategoriaFiltroRequest filtroRequest)
    {
        IEnumerable<Categoria> categorias = await GetAllAsync();
        IQueryable<Categoria> queryableCategoria = 
            categorias.OrderBy(c => c.Nome).AsQueryable();
        
        if (!string.IsNullOrEmpty(filtroRequest.Nome))
        {
            queryableCategoria = queryableCategoria.Where(c =>
                c.Nome != null && c.Nome.Contains(filtroRequest.Nome));
        }

        return await queryableCategoria.ToPagedListAsync(filtroRequest.PageNumber,
            filtroRequest.PageSize);
    }
}