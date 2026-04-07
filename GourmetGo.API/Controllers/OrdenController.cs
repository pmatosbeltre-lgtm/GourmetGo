using GourmetGo.Application.DTOs.Operaciones;
using GourmetGo.Domain.Entidades;
using GourmetGo.Persistence.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace GourmetGo.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] 
    public class OrdenController : ControllerBase
    {
        private readonly GourmetGoContext _context;

        public OrdenController(GourmetGoContext context)
        {
            _context = context;
        }

        // GET: api/Orden
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Orden>>> GetOrdenes()
        {
            return await _context.Ordenes.ToListAsync();
        }

        // GET: api/Orden/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Orden>> GetOrden(int id)
        {
            var orden = await _context.Ordenes.FindAsync(id);

            var orden = await _ordenService.ObtenerOrdenPorIdAsync(id);

            if (orden == null)
                return NotFound();

            return orden;
        }

        // POST: api/Orden
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateOrdenDTO dto)
        {
            var orden = new Orden(dto.Fecha, dto.TipoOrden, dto.RestauranteId, dto.UsuarioId );

            _context.Ordenes.Add(orden);
            await _context.SaveChangesAsync();

            return Ok(orden);
        }

        // PUT: api/Orden/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateOrdenDTO dto)
        {
            // 1. Buscar la orden existente en la base de datos
            var ordenExistente = await _context.Ordenes.FindAsync(id);

            // 2. Si no se encuentra, devolver un error 404 Not Found
            if (ordenExistente == null)
            {
                return NotFound();
            }

            // 4. Guardar los cambios en la base de datos
            await _context.SaveChangesAsync();

            // 5. Devolver una respuesta exitosa (204 No Content es estándar para PUT)
            return NoContent();
        }

    }
}
