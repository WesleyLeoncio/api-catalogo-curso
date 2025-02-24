namespace api_catalogo_curso.infra.config;

public static class PolicyCorsConfig
{
    public static void AddPolicyCors(this IServiceCollection service)
    {
        service.AddCors(options =>
        {
            options.AddDefaultPolicy(policy =>
            {
                policy.WithOrigins("https://apirequest.io")
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithExposedHeaders("Authorization");
            });
        });
    }
}