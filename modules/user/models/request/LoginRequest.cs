using System.ComponentModel.DataAnnotations;

namespace api_catalogo_curso.modules.user.models.request;

public class LoginRequest
{
    [Required(ErrorMessage ="O campo UserName é Obrigatorio!")]
    public string? UserName { get; set; }

    [Required(ErrorMessage = "O campo Password é Obrigatorio!")]
    public string? Password { get; set; }

}