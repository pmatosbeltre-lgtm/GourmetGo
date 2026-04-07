using GourmetGo.Application.Base;
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
        _notificacionRepositorio = notificacionRepositorio;
    }

    public async Task<Result<NotificacionDTO>> CrearNotificacionAsync(CreateNotificacionDTO dto)
    {
        if (dto is null)
            return Result<NotificacionDTO>.Fail("La notificación no puede estar vacía.");

        if (dto.UsuarioId <= 0)
            return Result<NotificacionDTO>.Fail("UsuarioId inválido.");

        if (string.IsNullOrWhiteSpace(dto.Tipo))
            return Result<NotificacionDTO>.Fail("Tipo inválido.");

        if (string.IsNullOrWhiteSpace(dto.Mensaje))
            return Result<NotificacionDTO>.Fail("Mensaje inválido.");

        var notificacion = new Notificacion(dto.Tipo, dto.Mensaje, dto.UsuarioId);

        await _notificacionRepositorio.AgregarAsync(notificacion);

        return Result<NotificacionDTO>.Ok(MapToDTO(notificacion), "Notificación creada correctamente.");
    }

    public async Task<Result<List<NotificacionDTO>>> ObtenerNotificacionesPorUsuarioAsync(int usuarioId)
    {
        if (usuarioId <= 0)
            return Result<List<NotificacionDTO>>.Fail("UsuarioId inválido.");

        var notificaciones = await _notificacionRepositorio.ObtenerPorUsuarioAsync(usuarioId);

        var data = notificaciones.Select(MapToDTO).ToList();

        return Result<List<NotificacionDTO>>.Ok(data);
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