using GourmetGo.Application.DTOs.Operaciones;
using GourmetGo.Application.Interfaces.Operaciones;
using GourmetGo.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GourmetGo.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class DetalleOrdenController : ControllerBase
{
    private readonly IDetalleOrdenRepositorio _detalleOrdenRepositorio;

    public DetalleOrdenController(IDetalleOrdenRepositorio detalleOrdenRepositorio)
    {
        _detalleOrdenRepositorio = detalleOrdenRepositorio;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateDetalleOrdenDTO dto)
    {
        if (dto == null || dto.OrdenId <= 0 || dto.PlatoId <= 0)
            return BadRequest("Datos inválidos");

        var detalleOrden = new GourmetGo.Domain.Entidades.DetalleOrden(
            dto.OrdenId,
            dto.PlatoId,
            dto.Cantidad,
            dto.PrecioUnitario
        );

        await _detalleOrdenRepositorio.AgregarAsync(detalleOrden);
        return Ok(detalleOrden);
    }
}