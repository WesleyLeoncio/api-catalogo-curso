using System.ComponentModel.DataAnnotations;

namespace api_catalogo_curso.modules.user.models.request;

public class RegisterRequest
{
    [Required(ErrorMessage ="O campo UserName é Obrigatorio!")]
    public string? Username { get; set; }

    [EmailAddress]
    [Required(ErrorMessage ="O campo Email é Obrigatorio!")]
    public string? Email { get; set; }

    [Required(ErrorMessage ="O campo Password é Obrigatorio!")]
    public string? Password { get; set; }
}