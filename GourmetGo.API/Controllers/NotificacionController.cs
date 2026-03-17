using GourmetGo.Application.DTOs.Social;
using GourmetGo.Application.Interfaces.Social;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GourmetGo.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class NotificacionController : ControllerBase
{
    private readonly INotificacionService _notificacionService;

    public NotificacionController(INotificacionService notificacionService)
    {
        _notificacionService = notificacionService;
    }

    [HttpGet("usuario/{usuarioId}")]
    public async Task<IActionResult> GetPorUsuario(int usuarioId)
    {
        if (usuarioId <= 0)
            return BadRequest("Usuario inválido");

        var notificaciones = await _notificacionService.ObtenerNotificacionesPorUsuarioAsync(usuarioId);
        return Ok(notificaciones);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateNotificacionDTO dto)
    {
        if (dto == null || string.IsNullOrWhiteSpace(dto.Mensaje))
            return BadRequest("Datos inválidos");

        var notificacion = await _notificacionService.CrearNotificacionAsync(dto);
        return Ok(notificacion);
    }
}