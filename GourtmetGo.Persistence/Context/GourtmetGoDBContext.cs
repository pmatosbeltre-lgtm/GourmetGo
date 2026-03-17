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

        modelBuilder.Entity<Usuario>().ToTable("Usuario");
        modelBuilder.Entity<Restaurante>().ToTable("Restaurante");
        modelBuilder.Entity<Menu>().ToTable("Menu");
        modelBuilder.Entity<Plato>().ToTable("Plato");
        modelBuilder.Entity<Reserva>().ToTable("Reserva");
        modelBuilder.Entity<Orden>().ToTable("Orden");
        modelBuilder.Entity<Pago>().ToTable("Pago");
        modelBuilder.Entity<DetalleOrden>().ToTable("DetalleOrden");
        modelBuilder.Entity<Notificacion>(entity =>
        {
            // Le decimos a EF que una Notificacion tiene UNA relación con Usuario...
            entity.HasOne(n => n.Usuario)
                  // ...y que un Usuario puede tener MUCHAS notificaciones (si tienes una lista en Usuario, ponla aquí. Si no, déjalo vacío).
                  .WithMany()
                  // ¡LA LÍNEA CLAVE! Especificamos que la clave foránea para la relación anterior es la propiedad UsuarioId.
                  .HasForeignKey(n => n.UsuarioId);
        });
        modelBuilder.Entity<Reseña>().ToTable("Resena"); 
        modelBuilder.Entity<AuditoriaEntity>().ToTable("AuditoriaEntity");


    }
    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<Reserva> Reservas => Set<Reserva>();
    public DbSet<Orden> Ordenes => Set<Orden>();
    public DbSet<Pago> Pago => Set<Pago>();
    public DbSet<DetalleOrden> DetalleOrden => Set<DetalleOrden>();
    public DbSet<Menu> Menus => Set<Menu>();
    public DbSet<Plato> Platos => Set<Plato>();
    public DbSet<Restaurante> Restaurantes => Set<Restaurante>();
    public DbSet<Notificacion> Notificaciones => Set<Notificacion>();
    public DbSet<Reseña> Resenas => Set<Reseña>();
    public DbSet<AuditoriaEntity> Auditorias => Set<AuditoriaEntity>();



}

   