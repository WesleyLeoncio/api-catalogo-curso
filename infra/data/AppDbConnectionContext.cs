using api_catalogo_curso.modules.categoria.models.entity;
using api_catalogo_curso.modules.produto.models.entity;
using api_catalogo_curso.modules.user;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api_catalogo_curso.infra.data;

public class AppDbConnectionContext(DbContextOptions options) : 
    IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Categoria>? CategoriaBd { get; set; }
    
    public DbSet<Produto>? ProdutoBd { get; set; }
}