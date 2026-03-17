using GourmetGo.Application.DTOs.Social;
using GourmetGo.Domain.Entidades;
using GourmetGo.Persistence.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class NotificacionController : ControllerBase
{
    private readonly GourmetGoContext _context;

    public NotificacionController(GourmetGoContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Notificacion>>> GetNotificacion()
    {
        return await _context.Notificaciones.ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] CreateNotificacionDTO dto)
    {
        var notificaciones = new Notificacion(dto.Mensaje, dto.Tipo, dto.UsuarioId);

        _context.Notificaciones.Add(notificaciones);
        await _context.SaveChangesAsync();

        return Ok(notificaciones);
    }
}