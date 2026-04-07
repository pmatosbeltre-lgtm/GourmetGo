using GourmetGo.Application.Base;
using GourmetGo.Application.DTOs.Social;

namespace GourmetGo.Application.Interfaces.Social;

public interface INotificacionService
{
    Task<Result<NotificacionDTO>> CrearNotificacionAsync(CreateNotificacionDTO dto);

    Task<Result<List<NotificacionDTO>>> ObtenerNotificacionesPorUsuarioAsync(int usuarioId);
}