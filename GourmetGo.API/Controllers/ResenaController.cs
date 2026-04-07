using GourmetGo.Application.DTOs.Social;
using GourmetGo.Domain.Entidades;
using GourmetGo.Persistence.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GourmetGo.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
[Authorize]
    public class ResenaController : ControllerBase
    {
        private readonly GourmetGoContext _context;

        public ResenaController(GourmetGoContext context)
        {
            _context = context;
        }

        // GET: api/Resena
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var resenas = await _context.Resenas
                .AsNoTracking()
                .Select(r => new
                {
                    id = r.Id,
                    usuarioId = r.UsuarioId,
                    restauranteId = r.RestauranteId,
                    calificacion = r.Calificacion,
                    comentario = r.Comentario,
                    fecha = r.Fecha
                })
                .ToListAsync();

        var resenas = await _resenaService.ObtenerResenasPorRestauranteAsync(restauranteId);
            return Ok(resenas);
        }

        // GET: api/Resena/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetById(int id)
        {
            var r = await _context.Resenas
                .AsNoTracking()
                .Where(x => x.Id == id)
                .Select(x => new
                {
                    id = x.Id,
                    usuarioId = x.UsuarioId,
                    restauranteId = x.RestauranteId,
                    calificacion = x.Calificacion,
                    comentario = x.Comentario,
                    fecha = x.Fecha
                })
                .FirstOrDefaultAsync();

            return r is null ? NotFound() : Ok(r);
        }

        // POST: api/Resena
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateResenaDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Comentario))
                return BadRequest("Comentario is required.");

            var entidad = new Reseña(dto.UsuarioId, dto.RestauranteId, dto.Calificacion, dto.Comentario);

            _context.Resenas.Add(entidad);
            await _context.SaveChangesAsync();

            var response = new
            {
                id = entidad.Id,
                usuarioId = entidad.UsuarioId,
                restauranteId = entidad.RestauranteId,
                calificacion = entidad.Calificacion,
                comentario = entidad.Comentario,
                fecha = entidad.Fecha
            };

            return CreatedAtAction(nameof(GetById), new { id = entidad.Id }, response);
        }

        // DELETE: api/Resena/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var entidad = await _context.Resenas.FindAsync(id);
            if (entidad == null) return NotFound();

            _context.Resenas.Remove(entidad);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}