using System.Text;
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
using api_catalogo_curso.modules.token.service;
using api_catalogo_curso.modules.token.service.interfaces;
using api_catalogo_curso.modules.user;
using api_catalogo_curso.modules.user.models;
using api_catalogo_curso.modules.user.models.entity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//TODO TENTAR ORGANIZAR AS CONFIG

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

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbConnectionContext>()
    .AddDefaultTokenProviders();

// Config JWT
var secretKey = builder.Configuration["JWT:SecretKey"]
                ?? throw new ArgumentException("Invalid secret key!!");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ClockSkew = TimeSpan.Zero,
        ValidAudience = builder.Configuration["JWT:ValidAudience"],
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(secretKey))
    };
});
////////////////////////////////////////////////////////////////////////////////////////

// Config AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

// Config conection SGBD
builder.Services.AddDbContext<AppDbConnectionContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("Connection")));

// Injections, repositories 
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ITokenService, TokenService>();


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