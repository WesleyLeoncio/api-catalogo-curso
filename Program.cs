using api_catalogo_curso.infra.config;
using api_catalogo_curso.infra.data;
using api_catalogo_curso.infra.exceptions.handle;
using api_catalogo_curso.infra.exceptions.interfaces;
using api_catalogo_curso.infra.middlewares;
using api_catalogo_curso.modules.user.models.entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Configuração das opções JSON usando a classe JsonConfig
builder.Services.AddJsonConfiguration();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// Configuração do Swagger usando a classe SwaggerConfig
builder.Services.AddSwaggerConfiguration();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbConnectionContext>()
    .AddDefaultTokenProviders();


// Configuração do JWT usando a class JwtConfig
builder.Services.AddJwtConfiguration(builder.Configuration); 

// Configuração de Políticas de Autorização usando a class AuthorizationConfig
builder.Services.AddAuthorizationPolicies();

// Config AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

// Config conection SGBD
builder.Services.AddDbContext<AppDbConnectionContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Connection")));

// Configuração de injeções de dependência usando a class DependencyInjectionConfig
builder.Services.AddDependencyInjections();

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

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();