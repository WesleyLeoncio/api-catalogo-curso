namespace api_catalogo_curso.modules.token.models.request;

public class TokenRequest
{
    public string? AccessToken { get; set; }
    public string? RefreshToken { get; set; }
}