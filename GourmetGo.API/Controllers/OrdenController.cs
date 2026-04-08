using GourmetGo.Application.DTOs.Operaciones;
using GourmetGo.Application.Interfaces.Operaciones;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GourmetGo.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] 
    public class OrdenController : ControllerBase
    {
        private readonly IOrdenService _ordenService; 

        public OrdenController(IOrdenService ordenService)
        {
            _ordenService = ordenService ?? throw new ArgumentNullException(nameof(ordenService));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateOrdenDTO dto)
        {
            if (dto == null)
                return BadRequest("El DTO no puede estar vacío");

            var result = await _ordenService.CrearOrdenAsync(dto); 

            return Ok(result);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Get(int id)
        {
            if (id <= 0)
                return BadRequest("El id debe ser mayor que cero");

            var orden = await _ordenService.ObtenerOrdenPorIdAsync(id);

            if (orden == null)
                return NotFound("Orden no encontrada");

            return Ok(orden);
        }

        [HttpGet("usuario/{usuarioId}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByUsuario(int usuarioId)
        {
            if (usuarioId <= 0)
                return BadRequest("El id del usuario debe ser válido");

            var ordenes = await _ordenService.ObtenerOrdenesPorUsuarioAsync(usuarioId);

            return Ok(ordenes);
        }

        [HttpPut("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateOrdenDTO dto)
        {
            if (id <= 0)
                return BadRequest("El id debe ser mayor que cero");

            if (dto == null)
                return BadRequest("El DTO no puede estar vacío");

            var result = await _ordenService.ActualizarEstadoAsync(id, dto);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);

        }

        [HttpGet("restaurante/{restauranteId}")]
        [AllowAnonymous]
        public async Task<IActionResult> ObtenerPorRestaurante(int restauranteId)
        {
            if (restauranteId <= 0)
                return BadRequest(new { message = "ID inválido" });

            var ordenes = await _ordenService.ObtenerOrdenesPorRestauranteAsync(restauranteId);
            return Ok(ordenes);
        }
    }
}