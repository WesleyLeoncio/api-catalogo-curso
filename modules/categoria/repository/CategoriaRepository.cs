using api_catalogo_curso.infra.data;
using api_catalogo_curso.modules.categoria.models.entity;
using api_catalogo_curso.modules.categoria.repository.interfaces;
using api_catalogo_curso.modules.common.repository;

namespace api_catalogo_curso.modules.categoria.repository;

public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
{
    public CategoriaRepository(AppDbConnectionContext context) : base(context) { }
}