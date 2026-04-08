using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using GourmetGo.Application.Interfaces.Catalogo;
using GourmetGo.Application.DTOs.Catalogo;
using GourmetGo.Application.DTOs.Catalogo.Menu;
using System.Security.Claims;

namespace GourmetGo.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _menuService;

        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        // Obtener menús por restaurante
        [HttpGet("restaurante/{restauranteId}")]
        [AllowAnonymous]
        public async Task<IActionResult> ObtenerPorRestaurante(int restauranteId)
        {
            var result = await _menuService.ObtenerPorRestauranteAsync(restauranteId);

            return Ok(result);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var result = await _menuService.ObtenerPorIdAsync(id);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
        // Crear un nuevo menú
        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] CreateMenuDTO dto)
        {
            var result = await _menuService.CrearAsync(dto);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] UpdateMenuDTO dto)
        {
            var result = await _menuService.ActualizarAsync(id, dto);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var result = await _menuService.EliminarAsync(id);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

    }
}