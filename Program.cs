using System.Text.Json.Serialization;
using api_catalogo_curso.infra.data;
using api_catalogo_curso.infra.exceptions.handle;
using api_catalogo_curso.infra.exceptions.interfaces;
using api_catalogo_curso.infra.middlewares;
using api_catalogo_curso.modules.categoria.repository;
using api_catalogo_curso.modules.categoria.repository.interfaces;
using api_catalogo_curso.modules.common.repository;
using api_catalogo_curso.modules.common.repository.interfaces;
using api_catalogo_curso.modules.common.unit_of_work;
using api_catalogo_curso.modules.common.unit_of_work.interfaces;
using api_catalogo_curso.modules.produto.repository;
using api_catalogo_curso.modules.produto.repository.interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// CYCLONISAÇÃO
builder.Services.AddControllers()
    .AddJsonOptions(option =>
    {
        //Ignora ciclos quando detectados
        option.JsonSerializerOptions 
            .ReferenceHandler = ReferenceHandler.IgnoreCycles;
        //Converte valores de enumeração de/para cadeias de caractere
        option.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Config AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

// Config conection SGBD
builder.Services.AddDbContext<AppDbConnectionContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Connection")));

// Injections, repositories and services
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


// Handle Exceptions
builder.Services.AddTransient<IErrorResultTask, HandleNotFound>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware(typeof(GlobalErrorHandlingMiddleware));

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();