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

    [HttpGet("restaurante/{restauranteId}")]
    public async Task<IActionResult> GetPorRestaurante(int restauranteId)
    {
        if (restauranteId <= 0)
            return BadRequest("Restaurante inválido");

        var resenas = await _resenaService.ObtenerResenasPorRestauranteAsync(restauranteId);
        return Ok(resenas);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateResenaDTO dto)
    {
        if (dto == null || string.IsNullOrWhiteSpace(dto.Comentario))
            return BadRequest("Datos inválidos");

        var resena = await _resenaService.CrearResenaAsync(dto);
        return Ok(resena);
    }

    //[HttpDelete("{id}")]
    //public async Task<IActionResult> Delete(int id)
    //{
    //    if (id <= 0)
    //        return BadRequest("ID inválido");

    //    // Aquí necesitarías un método Delete en IResenaService
    //    return Ok("Reseña eliminada");
    }
}