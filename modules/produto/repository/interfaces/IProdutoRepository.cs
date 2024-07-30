using api_catalogo_curso.modules.common.pagination;
using api_catalogo_curso.modules.common.repository.interfaces;
using api_catalogo_curso.modules.produto.models.entity;
using api_catalogo_curso.modules.produto.models.request;

namespace api_catalogo_curso.modules.produto.repository.interfaces;

public interface IProdutoRepository : IRepository<Produto>
{
    PagedList<Produto> GetAllFilterPageable(ProdutoFiltroRequest filtroRequest);
}