using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using api_catalogo_curso.modules.produto.models.entity;

namespace api_catalogo_curso.modules.categoria.models.entity;

[Table("categorias")]
public class Categoria
{
    [Key]
    [Column(name:"id")]
    public int Id { get; set; }
    
    [Column(name:"nome")] 
    [Required]
    [StringLength(80)]
    public string? Nome { get; set; }
    
    [Column(name:"imagem_url")]
    [Required]
    [StringLength(300)]
    public string? ImagemUrl { get; set; }
    
    public ICollection<Produto> Produtos { get; set; } = new Collection<Produto>();
}