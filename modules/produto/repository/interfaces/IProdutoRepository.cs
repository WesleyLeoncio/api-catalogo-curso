using api_catalogo_curso.modules.common.repository.interfaces;
using api_catalogo_curso.modules.produto.models.entity;
using api_catalogo_curso.modules.produto.models.request;
using X.PagedList;

namespace api_catalogo_curso.modules.produto.repository.interfaces;

public interface IProdutoRepository : IRepository<Produto>
{
    Task<IPagedList<Produto>> GetAllFilterPageableAsync(ProdutoFiltroRequest filtroRequest);
}