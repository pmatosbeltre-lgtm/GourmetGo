using GourmetGo.Application.DTOs.Operaciones;
using GourmetGo.Domain.Entidades;
using GourmetGo.Persistence.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

[ApiController]
[Route("api/[controller]")]
public class PagoController : ControllerBase
{
    private readonly GourmetGoContext _context;

    public PagoController(GourmetGoContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Pago>>> Get() =>
        await _context.Pago.ToListAsync();

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] CreatePagoDTO dto)
    {
        var ordenExiste = await _context.Ordenes.AnyAsync(o => o.Id == dto.OrdenId);
        if (!ordenExiste) return BadRequest("OrdenId no existe.");

        var pago = new Pago(dto.Monto, dto.MetodoPago, dto.OrdenId);

        _context.Pago.Add(pago); 
        await _context.SaveChangesAsync();

        return Ok(new
        {
            id = pago.Id,
            metodoPago = pago.MetodoPago,
            monto = pago.Monto,
            ordenId = pago.OrdenId
        });
    
    }
}