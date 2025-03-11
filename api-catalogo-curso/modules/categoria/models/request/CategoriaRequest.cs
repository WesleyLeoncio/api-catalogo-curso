using System.ComponentModel.DataAnnotations;

namespace api_catalogo_curso.modules.categoria.models.request;

public record CategoriaRequest(
    [Required(ErrorMessage = "Campo Nome Obrigatorio!")] 
    [StringLength(80, ErrorMessage = "O campo Nome deve ter no máximo 80 caracteres!")]
    string Nome,
    [Required(ErrorMessage = "Campo ImagemUrl Obrigatorio!")] 
    [StringLength(300, ErrorMessage = "O campo ImagemUrl deve ter no máximo 300 caracteres!")]
    string ImagemUrl
);