using System.ComponentModel.DataAnnotations;

namespace api_catalogo_curso.modules.produto.models.request;

public record ProdutoRequest(
    
    [Required(ErrorMessage = "Campo Nome Obrigatorio!")] 
    [StringLength(80)] string? Nome,
   
    [Required(ErrorMessage = "Campo Descrição Obrigatorio!")]  
    [StringLength(300)] string? Descricao,
    
    [Range(1,20000, ErrorMessage = "O Preço deve está dentro do Range (1 a 20000)")]
    decimal Preco,
    
    [Required(ErrorMessage = "Campo ImagemUrl Obrigatorio!")] 
    [StringLength(300)] string? ImagemUrl,
    
    [Range(1,20000, ErrorMessage = "O Estoque deve está dentro do Range (1 a 20000)")]
    float Estoque,
    
    [Range(1,20000, ErrorMessage = "A CategoriaId deve ser informada e está dentro do Range (1 a 20000)")]
    int CategoriaId
    
    
);