using api_catalogo_curso.infra.data;
using api_catalogo_curso.modules.common.repository;
using api_catalogo_curso.modules.produto.models.entity;
using api_catalogo_curso.modules.produto.models.enums;
using api_catalogo_curso.modules.produto.models.request;
using api_catalogo_curso.modules.produto.repository.interfaces;
using X.PagedList;

namespace api_catalogo_curso.modules.produto.repository;

public class ProdutoRepository : Repository<Produto>, IProdutoRepository
{
    public ProdutoRepository(AppDbConnectionContext context) : base(context) { }
    public async Task<IPagedList<Produto>> GetAllFilterPageableAsync(ProdutoFiltroRequest filtroRequest)
    {
        IEnumerable<Produto> produtos = await GetAllAsync();
     
        if (filtroRequest.VerificarValores())
        {
            switch (filtroRequest.PrecoCriterio)
            {
                case Criterio.MAIOR:
                    produtos = produtos.Where(p => p.Preco > filtroRequest.Preco)
                        .OrderBy(p => p.Preco);
                    break;
                case Criterio.MENOR:
                    produtos = produtos.Where(p => p.Preco < filtroRequest.Preco)
                        .OrderBy(p => p.Preco);
                    break;
                case Criterio.IGUAL:
                    produtos = produtos.Where(p => p.Preco == filtroRequest.Preco)
                        .OrderBy(p => p.Preco);
                    break;
            }
            
        }
        return await produtos.ToPagedListAsync(filtroRequest.PageNumber,
            filtroRequest.PageSize);
    }

  
}