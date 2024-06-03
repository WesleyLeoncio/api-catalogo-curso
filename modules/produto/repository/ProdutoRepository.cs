using api_catalogo_curso.infra.data;
using api_catalogo_curso.modules.common.repository;
using api_catalogo_curso.modules.produto.models.entity;
using api_catalogo_curso.modules.produto.repository.interfaces;

namespace api_catalogo_curso.modules.produto.repository;

public class ProdutoRepository : Repository<Produto>, IProdutoRepository
{
    public ProdutoRepository(AppDbConnectionContext context) : base(context) { }
}