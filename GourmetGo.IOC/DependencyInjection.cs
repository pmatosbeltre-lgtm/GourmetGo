using Microsoft.Extensions.DependencyInjection;
using GourmetGo.Domain.Interfaces;
using GourmetGo.Persistence.Repositories.Seguridad;
using GourmetGo.Persistence.Repositories.Operaciones;
using GourmetGo.Persistence.Repositories.Catalogo;
using GourmetGo.Persistence.Repositories.Social;
using GourmetGo.Persistence.Context;

using GourmetGo.Application.Interfaces;
using GourmetGo.Application.Services;
using GourmetGo.Application.Interfaces.Operaciones;
using GourmetGo.Application.Services.Operaciones;
using GourmetGo.Application.Interfaces.Social;
using GourmetGo.Application.Services.Social;
using GourmetGo.Application.Services.Catalogo;
using GourmetGo.Application.Interfaces.Catalogo;
using GourmetGo.Application.Interfaces.Auditoria;
using GourmetGo.Application.Services.Auditoria;


namespace GourmetGo.IOC;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        // Repositorios de Seguridad
        services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();

        // Repositorios de Operaciones
        services.AddScoped<IReservaRepositorio, ReservaRepositorio>();
        services.AddScoped<IOrdenRepositorio, OrdenRepositorio>();
        services.AddScoped<IDetalleOrdenRepositorio, DetalleOrdenRepositorio>();
        services.AddScoped<IPagoRepositorio, PagoRepositorio>();

        //Repositorios de Catalogo
        services.AddScoped<IMenuRepositorio, MenuRepositorio>();
        services.AddScoped<IPlatoRepositorio, PlatoRepositorio>();
        services.AddScoped<IRestauranteRepositorio, RestauranteRepositorio>();

        //Repositorios de Social
        services.AddScoped<INotificacionRepositorio, NotificacionRepositorio>();
        services.AddScoped<IResenaRepositorio, ReseñaRepositorio>();

        //Repositorios de Auditoria
        services.AddScoped<IAuditoriaRepositorio, AuditoriaRepositorio>();

        //Servicios (aplicacion por modulo)
        //seguridad
        services.AddScoped<IUsuarioService, UsuarioService>();

        //operaciones
        services.AddScoped<IOrdenService, OrdenService>();
        services.AddScoped<IReservaService, ReservaService>();
        services.AddScoped<IPagoService, PagoService>();

        //social
        services.AddScoped<INotificacionService, NotificacionService>();
        services.AddScoped<IResenaService, ResenaService>();

        //catalogo
        services.AddScoped<IMenuService, MenuService>();
        services.AddScoped<IPlatoService, PlatoService>();  
        services.AddScoped<IRestauranteService, RestauranteService>();
        
        //auditoria
        services.AddScoped<IAuditoriaService, AuditoriaService>();

        return services;
    }
}