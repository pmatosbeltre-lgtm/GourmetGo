using GourmetGo.Domain.Base;


namespace GourmetGo.Domain.Entidades;

public class AuditoriaEntity : BaseEntity
{
    
    public int Id { get; set; }

    public string Accion { get; private set; }

    public DateTime Fecha { get; private set; }

    public int UsuarioId { get; private set; }

    public Usuario Usuario { get; private set; }

    public AuditoriaEntity(string accion, int usuarioId, DateTime fecha)
    {
        Accion = accion;
        UsuarioId = usuarioId;
        Fecha = fecha;
    }
}