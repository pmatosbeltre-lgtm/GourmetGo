using GourmetGo.Domain.Base;
using GourmetGo.Domain.Enums;

namespace GourmetGo.Domain.Entidades;

public class Usuario : BaseEntity
{
    public string Nombre { get; private set; }

    public string Correo { get; private set; }

    public string Contrasena { get; private set; }

    public RolUsuario Rol { get; private set; }

    public EstadoUsuario Estado { get; private set; }

    public DateTime FechaRegistro { get; private set; }

    //Relaciones

    public ICollection<Reserva> Reservas { get; private set; }

    public ICollection<Orden> Ordenes { get; private set; }

    public ICollection<Reseña> Reseñas { get; private set; }

    public ICollection<Notificacion> Notificaciones { get; private set; }

    public ICollection<Auditoria> Auditorias { get; private set; }

    public Usuario()
    {
        Reservas = new List<Reserva>();
        Ordenes = new List<Orden>();
        Reseñas = new List<Reseña>();
        Notificaciones = new List<Notificacion>();
        Auditorias = new List<Auditoria>();
    }
}