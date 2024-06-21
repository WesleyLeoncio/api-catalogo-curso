using api_catalogo_curso.modules.categoria.repository.interfaces;
using api_catalogo_curso.modules.produto.repository.interfaces;

namespace api_catalogo_curso.modules.common.unit_of_work.interfaces;

public interface IUnitOfWork
{
    ICategoriaRepository CategoriaRepository { get; }
    IProdutoRepository ProdutoRepository { get; }
    void Commit();
    void Dispose();
}