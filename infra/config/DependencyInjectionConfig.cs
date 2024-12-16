using api_catalogo_curso.infra.exceptions.handle;
using api_catalogo_curso.infra.exceptions.interfaces;
using api_catalogo_curso.modules.categoria.repository;
using api_catalogo_curso.modules.categoria.repository.interfaces;
using api_catalogo_curso.modules.common.repository;
using api_catalogo_curso.modules.common.repository.interfaces;
using api_catalogo_curso.modules.common.unit_of_work;
using api_catalogo_curso.modules.common.unit_of_work.interfaces;
using api_catalogo_curso.modules.produto.repository;
using api_catalogo_curso.modules.produto.repository.interfaces;
using api_catalogo_curso.modules.token.service;
using api_catalogo_curso.modules.token.service.interfaces;

namespace api_catalogo_curso.infra.config;

public static class DependencyInjectionConfig
{
    public static void AddDependencyInjections(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<ICategoriaRepository, CategoriaRepository>();
        services.AddScoped<IProdutoRepository, ProdutoRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IErrorResultTask, HandleNotFound>();
    }
}