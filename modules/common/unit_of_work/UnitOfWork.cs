using api_catalogo_curso.infra.data;
using api_catalogo_curso.modules.categoria.repository;
using api_catalogo_curso.modules.categoria.repository.interfaces;
using api_catalogo_curso.modules.common.unit_of_work.interfaces;
using api_catalogo_curso.modules.produto.repository;
using api_catalogo_curso.modules.produto.repository.interfaces;

namespace api_catalogo_curso.modules.common.unit_of_work;

public class UnitOfWork : IUnitOfWork
{
    private IProdutoRepository? _produtoRepository;
    private ICategoriaRepository? _categoriaRepository;
    private readonly AppDbConnectionContext _context;

    public UnitOfWork(AppDbConnectionContext context)
    {
        _context = context;
    }

    public IProdutoRepository ProdutoRepository
    {
        get
        {
           return _produtoRepository = _produtoRepository ?? new ProdutoRepository(_context);
        }
    }
    
    public ICategoriaRepository CategoriaRepository
    {
        get
        {
            return _categoriaRepository = _categoriaRepository ?? new CategoriaRepository(_context);
        }
    }

    public void Commit()
    {
        _context.SaveChanges();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}