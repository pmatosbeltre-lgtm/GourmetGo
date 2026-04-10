using GourmetGo.Application.DTOs.Operaciones;
using GourmetGo.Application.Interfaces.Operaciones;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GourmetGo.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PagoController : ControllerBase
{
    private readonly IPagoService _pagoService;

    public PagoController(IPagoService pagoService)
    {
        _pagoService = pagoService;
    }

    [HttpGet("orden/{ordenId:int}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetPorOrden(int ordenId)
    {
        if (ordenId <= 0)
            return BadRequest("El ordenId debe ser mayor que cero.");

        var result = await _pagoService.ObtenerPagoPorOrdenAsync(ordenId);

        if (!result.Success)
            return NotFound(result);

        return Ok(result);
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Post([FromBody] CreatePagoDTO dto)
    {
        if (dto is null)
            return BadRequest("El body no puede estar vacío.");

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _pagoService.RegistrarPagoAsync(dto);

        if (!result.Success)
            return BadRequest(result);

        return Ok(result);
    }
}