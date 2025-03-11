using api_catalogo_curso.modules.categoria.models.entity;
using api_catalogo_curso.modules.categoria.models.request;
using api_catalogo_curso.modules.common.pagination.models.request;
using api_catalogo_curso.modules.common.repository.interfaces;
using X.PagedList;

namespace api_catalogo_curso.modules.categoria.repository.interfaces;

public interface ICategoriaRepository : IRepository<Categoria>
{
    Task<IPagedList<Categoria>> GetAllIncludePageableAsync(QueryParameters queryParameters);
    
    Task<IPagedList<Categoria>> GetAllFilterPageableAsync(CategoriaFiltroRequest filtroRequest);
}