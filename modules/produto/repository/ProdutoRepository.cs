using api_catalogo_curso.infra.data;
using api_catalogo_curso.modules.common.pagination;
using api_catalogo_curso.modules.common.repository;
using api_catalogo_curso.modules.produto.models.entity;
using api_catalogo_curso.modules.produto.models.request;
using api_catalogo_curso.modules.produto.repository.interfaces;

namespace api_catalogo_curso.modules.produto.repository;

public class ProdutoRepository : Repository<Produto>, IProdutoRepository
{
    public ProdutoRepository(AppDbConnectionContext context) : base(context) { }
    public PagedList<Produto> GetAllFilterPageable(ProdutoFiltroRequest filtroRequest)
    {
        IQueryable<Produto> produtos = GetIQueryable().OrderBy(p => p.Preco);

        if (filtroRequest.VerificarValores())
        {
            switch (filtroRequest.PrecoCriterio)
            {
                case "maior":
                    produtos = produtos.Where(p => p.Preco > filtroRequest.Preco);
                    break;
                case "menor":
                    produtos = produtos.Where(p => p.Preco < filtroRequest.Preco);
                    break;
                case "igual":
                    produtos = produtos.Where(p => p.Preco == filtroRequest.Preco);
                    break;
            }
            
        }

        return  PagedList<Produto>.ToPagedList(produtos, filtroRequest.PageNumber,
            filtroRequest.PageSize);
    }
}