using System.Text.Json.Serialization;
using api_catalogo_curso.infra.data;
using api_catalogo_curso.modules.categoria.repository;
using api_catalogo_curso.modules.categoria.repository.interfaces;
using api_catalogo_curso.modules.common.repository;
using api_catalogo_curso.modules.common.repository.interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//TODO SE DER ERRO DE CYCLONISAÇÃO
// builder.Services.AddControllers()
//     .AddJsonOptions(option => option.JsonSerializerOptions
//         .ReferenceHandler = ReferenceHandler.IgnoreCycles);


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<AppDbConnectionContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Connection")));

builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();