using api_catalogo_curso.modules.categoria.models.entity;
using api_catalogo_curso.modules.produto.models.entity;
using Microsoft.EntityFrameworkCore;

namespace api_catalogo_curso.infra.data;

public class AppDbConnectionContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Categoria>? CategoriaBd { get; set; }
    
    public DbSet<Produto>? ProdutoBd { get; set; }
}