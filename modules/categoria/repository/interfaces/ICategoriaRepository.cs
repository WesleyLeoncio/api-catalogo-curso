using api_catalogo_curso.modules.categoria.models.entity;
using api_catalogo_curso.modules.categoria.models.request;
using api_catalogo_curso.modules.common.pagination;
using api_catalogo_curso.modules.common.pagination.models.request;
using api_catalogo_curso.modules.common.repository.interfaces;

namespace api_catalogo_curso.modules.categoria.repository.interfaces;

public interface ICategoriaRepository : IRepository<Categoria>
{
    Pageable<Categoria> GetAllIncludePageable(QueryParameters queryParameters);
    
    Pageable<Categoria> GetAllFilterPageable(CategoriaFiltroRequest filtroRequest);
}