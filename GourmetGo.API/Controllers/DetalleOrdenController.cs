using GourmetGo.Application.DTOs.Operaciones;
using GourmetGo.Application.Interfaces.Operaciones;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GourmetGo.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class DetalleOrdenController : ControllerBase
{
    private readonly IDetalleOrdenService _detalleOrdenService;

    public DetalleOrdenController(IDetalleOrdenService detalleOrdenService)
    {
        _detalleOrdenService = detalleOrdenService;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateDetalleOrdenDTO dto)
    {
        if (dto is null)
            return BadRequest("El body no puede estar vacío.");

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _detalleOrdenService.CrearAsync(dto);

        if (!result.Success)
            return BadRequest(result);

        return Ok(result);
    }
}