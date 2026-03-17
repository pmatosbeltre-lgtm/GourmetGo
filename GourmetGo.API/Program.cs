using GourmetGo.Application.Interfaces;
using GourmetGo.Application.Interfaces.Auditoria;
using GourmetGo.Application.Interfaces.Catalogo;
using GourmetGo.Application.Interfaces.Operaciones;
using GourmetGo.Application.Services;
using GourmetGo.Application.Services.Auditoria;
using GourmetGo.Application.Services.Catalogo;
using GourmetGo.Application.Services.Operaciones;
using GourmetGo.Domain.Entidades;
using GourmetGo.Domain.Interfaces;
using GourmetGo.Persistence.Context;
using GourmetGo.Persistence.Repositories.Catalogo;
using GourmetGo.Persistence.Repositories.Operaciones;
using GourmetGo.Persistence.Repositories.Seguridad;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
 //.AddJsonOptions(options =>
 // {
 //     options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
 // });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

//Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//DB
builder.Services.AddDbContext<GourmetGoContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

//services 
builder.Services.AddScoped<IRestauranteService, RestauranteService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IMenuService, MenuService>();
builder.Services.AddScoped<IPlatoService, PlatoService>();
builder.Services.AddScoped<IReservaService, ReservaService>();
builder.Services.AddScoped<IAuditoriaService, AuditoriaService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

// Repositorios
builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
builder.Services.AddScoped<IRestauranteRepositorio, RestauranteRepositorio>();
builder.Services.AddScoped<IMenuRepositorio, MenuRepositorio>();
builder.Services.AddScoped<IPlatoRepositorio, PlatoRepositorio>();
builder.Services.AddScoped<IReservaRepositorio, ReservaRepositorio>();
builder.Services.AddScoped<IAuditoriaRepositorio, AuditoriaRepositorio>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
