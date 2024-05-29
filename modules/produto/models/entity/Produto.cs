using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using api_catalogo_curso.modules.categoria.models.entity;

namespace api_catalogo_curso.modules.produto.models.entity;

[Table("produtos")]
public class Produto
{
    [Key]
    [Column(name:"id")]
    public int Id { get; set; }
    
    [Required]
    [Column(name:"nome",TypeName = "varchar(80)")] 
    public string? Nome { get; set; }
    
    [Required]
    [Column(name:"descricao",TypeName = "varchar(300)")] 
    public string? Descricao { get; set; }
    
    [Required]
    [Column(name:"preco", TypeName = "decimal(10,2)")] 
    public decimal Preco { get; set; }
    
    [Required]
    [Column(name:"imagem_url",TypeName = "varchar(300)")] 
    public string? ImagemUrl { get; set; }
    
    [Column(name:"estoque")] 
    public float Estoque { get; set; }
    
    [Required]
    [Column(name:"data_cadastro")] 
    public DateTime DataCadastro { get; set; }
    
    [Column(name:"categoria_id")] 
    public int CategoriaId { get; set; }
    public Categoria Categoria { get; set; }
}