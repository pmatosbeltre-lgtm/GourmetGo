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
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

    public DbSet<Usuario> Usuario => Set<Usuario>();
    public DbSet<Reserva> Reservas => Set<Reserva>();
    public DbSet<Orden> Ordenes => Set<Orden>();
    public DbSet<Pago> Pagos => Set<Pago>();
    public DbSet<DetalleOrden> DetallesOrden => Set<DetalleOrden>();
    public DbSet<Menu> Menu => Set<Menu>();
    public DbSet<Plato> Plato => Set<Plato>();
    public DbSet<Restaurante> Restaurante => Set<Restaurante>();
    public DbSet<Notificacion> Notificaciones => Set<Notificacion>();
    public DbSet<Reseña> Resenas => Set<Reseña>();
    public DbSet<AuditoriaEntity> Auditoria => Set<AuditoriaEntity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Usuario>().ToTable("Usuario");
        modelBuilder.Entity<Restaurante>().ToTable("Restaurante");
        modelBuilder.Entity<Menu>().ToTable("Menu");
        modelBuilder.Entity<Plato>().ToTable("Plato");
        modelBuilder.Entity<Reserva>().ToTable("Reserva");
        modelBuilder.Entity<Orden>().ToTable("Orden");
        modelBuilder.Entity<Pago>().ToTable("Pago");
        modelBuilder.Entity<DetalleOrden>().ToTable("DetalleOrden");
        modelBuilder.Entity<Notificacion>().ToTable("Notificacion");
        modelBuilder.Entity<Reseña>().ToTable("Resena"); 
        modelBuilder.Entity<AuditoriaEntity>().ToTable("AuditoriaEntity");
    }
}







   