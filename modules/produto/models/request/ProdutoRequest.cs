using System.ComponentModel.DataAnnotations;

namespace api_catalogo_curso.modules.produto.models.request;

public record ProdutoRequest(
    
    [Required(ErrorMessage = "Campo Nome Obrigatorio!")] 
    [StringLength(80)] string? Nome,
   
    [Required(ErrorMessage = "Campo Descrição Obrigatorio!")]  
    [StringLength(300)] string? Descricao,
    
    [Required(ErrorMessage = "Campo Preço Obrigatorio!")] 
    decimal Preco,
    
    [Required(ErrorMessage = "Campo ImagemUrl Obrigatorio!")] 
    [StringLength(300)] string? ImagemUrl,
    
    [Required(ErrorMessage = "Campo Estoque Obrigatorio!")] 
    float Estoque,
    
    [Required(ErrorMessage = "Campo CategoriaId Obrigatorio!")] 
    int CategoriaId
    
    
);