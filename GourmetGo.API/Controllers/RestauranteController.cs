using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using GourmetGo.Application.Interfaces.Catalogo;
using GourmetGo.Application.DTOs.Catalogo;
using GourmetGo.Application.DTOs.Catalogo.Restaurante;
using System.IdentityModel.Tokens.Jwt;

namespace GourmetGo.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class RestauranteController : ControllerBase
    {
        private readonly IRestauranteService _restauranteService;

        public RestauranteController(IRestauranteService restauranteService)
        {
            _restauranteService = restauranteService;
        }

        // Obtener todos los restaurantes
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ObtenerTodos()
        {
            var result = await _restauranteService.ObtenerTodosAsync();
            return Ok(result);
        }

        // Obtener restaurante por ID 
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var result = await _restauranteService.ObtenerPorIdAsync(id);

            if (!result.Success)
                return NotFound(result);

            return Ok(result);
        }
        [AllowAnonymous]
        [HttpGet("mio/{usuarioId:int}")]
        public async Task<IActionResult> ObtenerMio(int usuarioId)
        {
            if (usuarioId <= 0) return BadRequest("usuarioId inválido.");

            var result = await _restauranteService.ObtenerPorUsuarioIdAsync(usuarioId);
            if (!result.Success) return NotFound(result);

            return Ok(result);
        }
        // Crear un nuevo restaurante
        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] CreateRestauranteDTO dto)
        {
            var result = await _restauranteService.CrearAsync(dto);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
    }
}