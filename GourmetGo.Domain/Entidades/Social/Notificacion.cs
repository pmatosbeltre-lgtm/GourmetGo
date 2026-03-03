using GourmetGo.Domain.Base;
using GourmetGo.Domain.Excepciones;

namespace GourmetGo.Domain.Entidades;

public class Notificacion : BaseEntity
{
    public string Tipo { get; private set; }
    public string Mensaje { get; private set; }
    public DateTime FechaEnvio { get; private set; }

    public int UsuarioId { get; private set; }
    public Usuario Usuario { get; private set; }

    // Validaciones
    public Notificacion(string tipo, string mensaje, int usuarioId)
    {
        if (string.IsNullOrWhiteSpace(tipo))
            throw new ExcepcionDominio("El tipo de notificación es obligatorio.");
       if (string.IsNullOrWhiteSpace(mensaje))
            throw new ExcepcionDominio("El mensaje de la notificación es obligatorio.");
       if (usuarioId <= 0)
            throw new ExcepcionDominio("El ID del usuario es obligatorio.");

        Tipo = tipo;
        Mensaje = mensaje;
        UsuarioId = usuarioId;
        FechaEnvio = DateTime.UtcNow;
    }
}