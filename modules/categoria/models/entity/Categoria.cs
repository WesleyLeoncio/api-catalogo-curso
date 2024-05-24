using System.Collections.ObjectModel;
using api_catalogo_curso.modules.produto.models.entity;

namespace api_catalogo_curso.modules.categoria.models.entity;

public class Categoria
{
    public int Id { get; set; }
    
    public string? Nome { get; set; }
    
    public string? ImagemUrl { get; set; }
    
    public ICollection<Produto> Produtos { get; set; } = new Collection<Produto>();
}