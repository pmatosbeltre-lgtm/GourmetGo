using GourmetGo.Application.DTOs.Operaciones;
using GourmetGo.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GourmetGo.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PagoController : ControllerBase
{
    private readonly IPagoRepositorio _pagoRepositorio;
    private readonly IOrdenRepositorio _ordenRepositorio;

    public PagoController(IPagoRepositorio pagoRepositorio, IOrdenRepositorio ordenRepositorio)
    {
        _pagoRepositorio = pagoRepositorio;
        _ordenRepositorio = ordenRepositorio;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreatePagoDTO dto)
    {
        if (dto == null || dto.OrdenId <= 0)
            return BadRequest("Datos inválidos");

        var ordenExiste = await _ordenRepositorio.ObtenerPorIdAsync(dto.OrdenId);
        if (ordenExiste == null)
            return BadRequest("OrdenId no existe");

        var pago = new GourmetGo.Domain.Entidades.Pago(dto.Monto, dto.MetodoPago, dto.OrdenId);

        await _pagoRepositorio.AgregarAsync(pago);

        return Ok(new
        {
            id = pago.Id,
            metodoPago = pago.MetodoPago,
            monto = pago.Monto,
            ordenId = pago.OrdenId
        });
    }
}