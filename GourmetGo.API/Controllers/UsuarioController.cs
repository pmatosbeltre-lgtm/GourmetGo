using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using GourmetGo.Application.Interfaces;
using GourmetGo.Application.Dtos.Seguridad;
using GourmetGo.Application.DTOs.Seguridad;

namespace GourmetGo.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        // Registrar un nuevo usuario
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> CrearUsuario([FromBody] CreateUsuarioDTO dto)
        {
            var result = await _usuarioService.CrearUsuario(dto);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }

        // Obtener usuario por ID
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> ObtenerUsuario(int id)
        {
            var result = await _usuarioService.ObtenerUsuario(id);

            if (!result.Success)
                return NotFound(result);

            return Ok(result);
        }

        // Obtener usuario por correo
        [HttpGet("correo/{correo}")]
        [AllowAnonymous]
        public async Task<IActionResult> ObtenerPorCorreo(string correo)
        {
            var result = await _usuarioService.ObtenerUsuarioPorCorreo(correo);

            if (!result.Success)
                return NotFound(result);

            return Ok(result);
        }
    }
}