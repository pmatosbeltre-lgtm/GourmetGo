using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using GourmetGo.Application.Interfaces.Catalogo;
using GourmetGo.Application.DTOs.Catalogo;
using GourmetGo.Application.DTOs.Catalogo.Menu;

namespace GourmetGo.API.Controllers
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

        // Crear un nuevo menú
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Crear([FromBody] CreateMenuDTO dto)
        {
            var result = await _menuService.CrearAsync(dto);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
    }
}