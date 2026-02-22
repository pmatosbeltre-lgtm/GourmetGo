using GourmetGo.Domain.Entidades;

namespace GourmetGo.Domain.Interfaces;

public interface INotificacionRepositorio
{
    Task<IEnumerable<Notificacion>> ObtenerPorUsuarioAsync(int usuarioId);

    Task AgregarAsync(Notificacion notificacion);
}