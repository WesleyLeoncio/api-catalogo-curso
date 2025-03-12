using AutoMapper;

namespace api_catalogo_curso_teste.config;

public static class AutoMapperConfig
{
    public static IMapper Configure(Profile profile)
    {
        var config = new MapperConfiguration(cfg =>
        {
              cfg.AddProfile(profile);
        });

        return config.CreateMapper();
    }
}