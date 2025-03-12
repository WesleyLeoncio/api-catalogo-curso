using api_catalogo_curso.infra.data;
using api_catalogo_curso.modules.common.unit_of_work;
using api_catalogo_curso.modules.common.unit_of_work.interfaces;
using Microsoft.EntityFrameworkCore;

namespace api_catalogo_curso_teste.config;

public static class DatabaseConfig
{
    public static IUnitOfWork CreateInstanceUof()
    {
        DbContextOptions<AppDbConnectionContext> dbContextOptions = new DbContextOptionsBuilder<AppDbConnectionContext>()
            .UseInMemoryDatabase("TestDatabase")
            .Options;
        
        AppDbConnectionContext context = new AppDbConnectionContext(dbContextOptions);

        return new UnitOfWork(context);
    }
}