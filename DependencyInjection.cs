using Microsoft.Extensions.DependencyInjection;
using GourmetGo.Domain.Interfaces;
using GourmetGo.Persistence.Repositorios.Seguridad;
using GourmetGo.Persistence.Repositorios.Operaciones;
using GourmetGo.Persistence.Repositorios.Catalogo;
using GourmetGo.Persistence.Repositorios.Social;
using GourmetGo.Persistence.Repositorios.Auditoria;

using GourmetGo.Application.Interfaces;
using GourmetGo.Application.Services;
using GourmetGo.Application.Interfaces.Seguridad;
using GourmetGo.Application.Services.Seguridad;
using GourmetGo.Application.Interfaces.Operaciones;
using GourmetGo.Application.Services.Operaciones;
using GourmetGo.Application.Interfaces.Social;
using GourmetGo.Application.Services.Social;

using GourmetGo.Persistence.Context;

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

        //Servicios (aplicacion )
        services.AddScoped<IUsuarioService, UsuarioService>();
        services.AddScoped<IOrdenService, OrdenService>();
        services.AddScoped<IReservaService, ReservaService>();
        services.AddScoped<IPagoService, PagoService>();
        services.AddScoped<INotificacionService, NotificacionService>();
        services.AddScoped<IResenaService, ResenaService>();


        return services;
    }
}