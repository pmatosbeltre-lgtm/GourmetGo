using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using GourmetGo.Application.Interfaces.Operaciones;
using GourmetGo.Application.DTOs.Operaciones;

namespace GourmetGo.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
            try
            {
                var reserva = await _reservaService.CrearReservaAsync(dto);
                return Ok(reserva);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        // Obtener reserva por ID
       
        [HttpGet("{id}")]
       
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            try
            {
                var reserva = await _reservaService.ObtenerReservaPorIdAsync(id);

                if (reserva == null)
                    return NotFound(new { mensaje = "Reserva no encontrada" });

                return Ok(reserva);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }

        // Obtener reservas por restaurante
        
        [HttpGet("restaurante/{restauranteId}")]
        
        public async Task<IActionResult> ObtenerPorRestaurante(int restauranteId)
        {
            try
            {
                var reservas = await _reservaService.ObtenerReservasPorRestauranteAsync(restauranteId);
                return Ok(reservas);
            }
            catch (Exception ex)
            {
                return BadRequest(new { mensaje = ex.Message });
            }
        }
    }
}