using api_catalogo_curso.infra.data;
using api_catalogo_curso.modules.categoria.models.entity;
using api_catalogo_curso.modules.categoria.repository.interfaces;
using api_catalogo_curso.modules.common.models;
using api_catalogo_curso.modules.common.repository;
using Microsoft.EntityFrameworkCore;

namespace api_catalogo_curso.modules.categoria.repository;

public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
{
    public CategoriaRepository(AppDbConnectionContext context) : base(context) { }
    
    // public IEnumerable<Categoria> GetAllInclude(int skip = 0, int take = 10)
    // {
    //     return GetIQueryable()
    //         .Include(categoria => categoria.Produtos)
    //         .Skip(skip).Take(take).ToList();
    // }
    
    public PagedList<Categoria> GetAllInclude(QueryParameters queryParameters)
    {
        IQueryable<Categoria> categorias = 
            GetIQueryable().OrderBy(c => c.Nome)
                .Include(categoria => categoria.Produtos);
        return PagedList<Categoria>
            .ToPagedList(categorias, queryParameters.PageNumber, queryParameters.PageSize);
    }
    
}