using System.ComponentModel.DataAnnotations;

namespace api_catalogo_curso.modules.categoria.models.request;

public record CategoriaRequest(
    [Required] [StringLength(80)] string Nome,
    [Required] [StringLength(300)] string ImagemUrl
);