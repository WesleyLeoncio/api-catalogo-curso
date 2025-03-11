using System.Text.Json.Serialization;

namespace api_catalogo_curso.infra.config;

public static class JsonConfig
{
    public static void AddJsonConfiguration(this IServiceCollection services)
    {
        services.AddControllers()
            .AddJsonOptions(option =>
            {
                // Ignora ciclos quando detectados
                option.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                    
                // Converte valores de enumeração de/para cadeias de caractere
                option.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
    }
}