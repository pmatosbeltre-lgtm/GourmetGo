using GourmetGo.Application.DTOs.Social;

namespace GourmetGo.Application.Interfaces.Social;

public interface INotificacionService
{
    Task<NotificacionDTO> CrearNotificacionAsync(CreateNotificacionDTO dto);

    Task<IEnumerable<NotificacionDTO>> ObtenerNotificacionesPorUsuarioAsync(int usuarioId);
}