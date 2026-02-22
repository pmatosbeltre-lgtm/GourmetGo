using GourmetGo.Domain.Base;

namespace GourmetGo.Domain.Entidades;

public class Notificacion : BaseEntity
{
    public string Tipo { get; private set; }

    public string Mensaje { get; private set; }

    public DateTime FechaEnvio { get; private set; }

    public int UsuarioId { get; private set; }

    public Usuario Usuario { get; private set; }
}