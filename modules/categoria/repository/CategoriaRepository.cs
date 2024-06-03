using api_catalogo_curso.infra.data;
using api_catalogo_curso.modules.categoria.models.entity;
using api_catalogo_curso.modules.categoria.repository.interfaces;
using api_catalogo_curso.modules.common.repository;
using Microsoft.EntityFrameworkCore;

namespace api_catalogo_curso.modules.categoria.repository;

public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
{
    private readonly AppDbConnectionContext _context;
    public CategoriaRepository(AppDbConnectionContext context) : base(context)
    {
        _context = context;
    }
    
    public IEnumerable<Categoria> GetAllInclude(int skip = 0, int take = 10)
    {
        return _context.CategoriaBd!.AsNoTracking()
            .Include(categoria => categoria.Produtos)
            .Skip(skip).Take(take);
    }
}