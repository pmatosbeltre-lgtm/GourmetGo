using GourmetGo.Application.DTOs.Social;
using GourmetGo.Application.Interfaces.Social;
using GourmetGo.Domain.Entidades;
using GourmetGo.Domain.Interfaces;

namespace GourmetGo.Application.Services.Social;

public class NotificacionService : INotificacionService
{
    private readonly INotificacionRepositorio _notificacionRepositorio;

    public NotificacionService(INotificacionRepositorio notificacionRepositorio)
    {
        _notificacionRepositorio = notificacionRepositorio
            ?? throw new ArgumentNullException(nameof(notificacionRepositorio));
    }

    public async Task<NotificacionDTO> CrearNotificacionAsync(CreateNotificacionDTO dto)
    {
        if (dto == null)
            throw new ArgumentNullException(nameof(dto));

        if (string.IsNullOrWhiteSpace(dto.Tipo))
            throw new ArgumentException("Tipo de notificación inválido.");

        if (string.IsNullOrWhiteSpace(dto.Mensaje))
            throw new ArgumentException("Mensaje inválido.");

        if (dto.UsuarioId <= 0)
            throw new ArgumentException("Usuario inválido.");

        var notificacion = new Notificacion(
            dto.Tipo,
            dto.Mensaje,
            dto.UsuarioId
        );

        await _notificacionRepositorio.AgregarAsync(notificacion);

        return MapToDTO(notificacion);
    }

    public async Task<IEnumerable<NotificacionDTO>> ObtenerNotificacionesPorUsuarioAsync(int usuarioId)
    {
        if (usuarioId <= 0)
            throw new ArgumentException("Usuario inválido.");

        var notificaciones = await _notificacionRepositorio.ObtenerPorUsuarioAsync(usuarioId);

        return notificaciones.Select(MapToDTO);
    }

    private static NotificacionDTO MapToDTO(Notificacion notificacion)
    {
        return new NotificacionDTO
        {
            Id = notificacion.Id,
            Tipo = notificacion.Tipo,
            Mensaje = notificacion.Mensaje,
            FechaEnvio = notificacion.FechaEnvio,
            UsuarioId = notificacion.UsuarioId
        };
    }
}