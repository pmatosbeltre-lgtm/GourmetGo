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
    
    [HttpGet("usuario/{usuarioId:int}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetPorUsuario(int usuarioId)
    {
        if (usuarioId <= 0)
            return BadRequest("El usuarioId debe ser mayor que cero.");

        var result = await _notificacionService.ObtenerNotificacionesPorUsuarioAsync(usuarioId);

        if (!result.Success)
            return BadRequest(result);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateNotificacionDTO dto)
    {
        if (dto is null)
            return BadRequest("El body no puede estar vacío.");

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _notificacionService.CrearNotificacionAsync(dto);

        if (!result.Success)
            return BadRequest(result);

        return Ok(result);
    }
}