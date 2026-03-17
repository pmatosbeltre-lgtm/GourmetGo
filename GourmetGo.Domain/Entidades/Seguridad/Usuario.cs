using GourmetGo.Domain.Base;
using GourmetGo.Domain.Enums;
using GourmetGo.Domain.Excepciones;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
    public ICollection<AuditoriaEntity> Auditorias { get; private set; }

    // Validaciones
    public Usuario(string nombre, string correo, string contrasena, RolUsuario rol)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            throw new UsuarioInvalidoExcepcion("El nombre es obligatorio.");
        if (string.IsNullOrWhiteSpace(correo))
            throw new UsuarioInvalidoExcepcion("El correo es obligatorio.");
        if (string.IsNullOrWhiteSpace(contrasena))
            throw new UsuarioInvalidoExcepcion("La contraseña es obligatoria.");

        Nombre = nombre;
        Correo = correo;
        Contrasena = contrasena;
        Rol = rol;
        Estado = EstadoUsuario.Activo;
        FechaRegistro = DateTime.UtcNow;

        Reservas = new List<Reserva>();
        Ordenes = new List<Orden>();
        Reseñas = new List<Reseña>();
        Notificaciones = new List<Notificacion>();
        Auditorias = new List<AuditoriaEntity>();
    }
}