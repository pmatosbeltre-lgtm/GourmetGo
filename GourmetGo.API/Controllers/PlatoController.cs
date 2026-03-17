using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using GourmetGo.Application.Interfaces.Catalogo;
using GourmetGo.Application.DTOs.Catalogo;
using GourmetGo.Application.DTOs.Catalogo.Plato;

namespace GourmetGo.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlatoController : ControllerBase
    {
        private readonly IPlatoService _platoService;

        public PlatoController(IPlatoService platoService)
        {
            _platoService = platoService;
        }

        // Obtener platos por menú
        
        [HttpGet("menu/{menuId}")]
        [AllowAnonymous]
        public async Task<IActionResult> ObtenerPorMenu(int menuId)
        {
            var result = await _platoService.ObtenerPorMenuAsync(menuId);

            return Ok(result);
        }

        // Crear un nuevo plato
        
        [HttpPost]
        
        public async Task<IActionResult> Crear([FromBody] CreatePlatoDTO dto)
        {
            var result = await _platoService.CrearAsync(dto);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
    }
}