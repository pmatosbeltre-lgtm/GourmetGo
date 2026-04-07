using GourmetGo.Application.DTOs.Social;
using GourmetGo.Application.Interfaces.Social;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GourmetGo.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ResenaController : ControllerBase
{
    private readonly IResenaService _resenaService;

    public ResenaController(IResenaService resenaService)
    {
        _resenaService = resenaService;
    }

    [HttpGet("restaurante/{restauranteId:int}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetPorRestaurante(int restauranteId)
    {
        if (restauranteId <= 0)
            return BadRequest("El restauranteId debe ser mayor que cero.");

        var result = await _resenaService.ObtenerResenasPorRestauranteAsync(restauranteId);

        if (!result.Success)
            return BadRequest(result);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateResenaDTO dto)
    {
        if (dto is null)
            return BadRequest("El body no puede estar vacío.");

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var result = await _resenaService.CrearResenaAsync(dto);

        if (!result.Success)
            return BadRequest(result);

        return Ok(result);
    }
}