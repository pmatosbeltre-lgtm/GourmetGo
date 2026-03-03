using GourmetGo.Domain.Entidades;
using GourmetGo.Domain.Entidades.Catalogo;
using Microsoft.EntityFrameworkCore;

namespace GourmetGo.Persistence.Context;

public class GourmetGoContext : DbContext
{
    public GourmetGoContext(DbContextOptions<GourmetGoContext> options)
        : base(options)
    {
    }


    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<Reserva> Reservas => Set<Reserva>();
    public DbSet<Orden> Ordenes => Set<Orden>();
    public DbSet<Pago> Pagos => Set<Pago>();
    public DbSet<DetalleOrden> DetallesOrden => Set<DetalleOrden>();
    public DbSet<Menu> Menus => Set<Menu>();
    public DbSet<Plato> Platos => Set<Plato>();
    public DbSet<Restaurante> Restaurantes => Set<Restaurante>();
    public DbSet<Notificacion> Notificaciones => Set<Notificacion>();
    public DbSet<Reseña> Resenas => Set<Reseña>();
    public DbSet<Auditoria> Auditorias => Set<Auditoria>();
}

   