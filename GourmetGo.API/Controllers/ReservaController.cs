using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using GourmetGo.Application.Interfaces.Operaciones;
using GourmetGo.Application.DTOs.Operaciones;

namespace GourmetGo.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] 
    public class ReservaController : ControllerBase
    {
        private readonly IReservaService _reservaService;

        public ReservaController(IReservaService reservaService)
        {
            _reservaService = reservaService;
        }

        // Crear una reserva
        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] CreateReservaDTO dto)
        {
            var result = await _reservaService.CrearReservaAsync(dto);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        // Obtener reserva por ID
        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var result = await _reservaService.ObtenerReservaPorIdAsync(id);

            if (!result.Success)
                return NotFound(result); 

            return Ok(result);
        }

        // Obtener reservas por restaurante
        [HttpGet("restaurante/{restauranteId}")]
        public async Task<IActionResult> ObtenerPorRestaurante(int restauranteId)
        {
            var result = await _reservaService.ObtenerReservasPorRestauranteAsync(restauranteId);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
    }
}