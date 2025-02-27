using System.Reflection;
using Microsoft.OpenApi.Models;

namespace api_catalogo_curso.infra.config;

public static class SwaggerConfig
{
    public static void AddSwaggerConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSwaggerGen(c =>
        {
           
            IConfigurationSection swaggerConfig = configuration.GetSection("Swagger");
            c.SwaggerDoc(swaggerConfig["Version"], new OpenApiInfo
            {
                Title = swaggerConfig["Title"],
                Version = swaggerConfig["Version"],
                Description = swaggerConfig["Description"],
                Contact = new OpenApiContact
                {
                    Name = swaggerConfig["Contact:Name"],
                    Email = swaggerConfig["Contact:Email"],
                    Url = new Uri(swaggerConfig["Contact:Url"] ?? string.Empty)
                }
            });

            var xmlFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFileName));
            
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Bearer JWT ",
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] {}
                }
            });
        });
    }
}

