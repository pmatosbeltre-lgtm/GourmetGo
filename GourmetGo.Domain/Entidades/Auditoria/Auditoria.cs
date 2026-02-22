using GourmetGo.Domain.Base;

namespace GourmetGo.Domain.Entidades;

public class Auditoria : BaseEntity
{
    public string Accion { get; private set; }

    public DateTime Fecha { get; private set; }

    public int UsuarioId { get; private set; }

    public Usuario Usuario { get; private set; }
}