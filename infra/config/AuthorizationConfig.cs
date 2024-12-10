namespace api_catalogo_curso.infra.config;

public static class AuthorizationConfig
{
    public static void AddAuthorizationPolicies(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy("MASTER", policy => policy.RequireRole("MASTER"));
            options.AddPolicy("ADMIN", policy => policy.RequireRole("ADMIN"));
            options.AddPolicy("USER", policy => policy.RequireRole("USER"));
        });
    }
}