using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using GourmetGo.Application.Interfaces.Auditoria;
using GourmetGo.Application.DTOs.Auditoria;

namespace GourmetGo.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class AuditoriaController : ControllerBase
    {
        private readonly IAuditoriaService _auditoriaService;

        public AuditoriaController(IAuditoriaService auditoriaService)
        {
            _auditoriaService = auditoriaService;
        }

        // Registrar acción de auditoría
        
        [HttpPost]
        
        public async Task<IActionResult> Registrar([FromBody] CreateAuditoriaDTO dto)
        {
            var result = await _auditoriaService.RegistrarAsync(dto);

            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
