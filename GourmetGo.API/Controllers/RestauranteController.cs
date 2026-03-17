using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using GourmetGo.Application.Interfaces.Catalogo;
using GourmetGo.Application.DTOs.Catalogo;
using GourmetGo.Application.DTOs.Catalogo.Restaurante;

namespace GourmetGo.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
        
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            var result = await _restauranteService.ObtenerPorIdAsync(id);

            if (!result.Success)
                return NotFound(result);

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