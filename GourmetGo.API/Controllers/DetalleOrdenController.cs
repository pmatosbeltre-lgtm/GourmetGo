using GourmetGo.Application.DTOs.Operaciones;
using GourmetGo.Domain.Entidades;
using GourmetGo.Persistence.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace GourmetGo.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class DetalleOrdenController : ControllerBase
{
    private readonly GourmetGoContext _context;

    public DetalleOrdenController(GourmetGoContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DetalleOrden>>> Get() =>
        await _context.DetalleOrden.ToListAsync();

    [HttpGet("{id}")]
    public async Task<ActionResult<DetalleOrden>> Get(int id)
    {
        var item = await _context.DetalleOrden.FindAsync(id);
        return item == null ? NotFound() : item;
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] CreateDetalleOrdenDTO dto)
    {
        var obj = new DetalleOrden(dto.OrdenId, dto.PlatoId, dto.Cantidad, dto.PrecioUnitario);

        _context.DetalleOrden.Add(obj);
        await _context.SaveChangesAsync();

        return Ok(obj);
    }

   

}