using GourmetGo.Application.Base;
using GourmetGo.Application.DTOs.Operaciones;
using GourmetGo.Application.Interfaces.Operaciones;
using GourmetGo.Domain.Entidades;
using GourmetGo.Domain.Interfaces;

namespace GourmetGo.Application.Services.Operaciones;

public class PagoService : IPagoService
{
    private readonly IPagoRepositorio _pagoRepositorio;

    public PagoService(IPagoRepositorio pagoRepositorio)
    {
        _pagoRepositorio = pagoRepositorio;
    }

    public async Task<Result<PagoDTO>> RegistrarPagoAsync(CreatePagoDTO dto)
    {
        if (dto is null)
            return Result<PagoDTO>.Fail("El pago no puede estar vacío.");

        if (dto.OrdenId <= 0)
            return Result<PagoDTO>.Fail("OrdenId inválido.");

        if (dto.Monto <= 0)
            return Result<PagoDTO>.Fail("Monto inválido.");

        if (string.IsNullOrWhiteSpace(dto.MetodoPago))
            return Result<PagoDTO>.Fail("Método de pago inválido.");

        // Regla de negocio opcional (recomendada): evitar doble pago por orden
        var existente = await _pagoRepositorio.ObtenerPorOrdenAsync(dto.OrdenId);
        if (existente is not null)
            return Result<PagoDTO>.Fail("Ya existe un pago registrado para esta orden.");

        var pago = new Pago(dto.Monto, dto.MetodoPago, dto.OrdenId);

        await _pagoRepositorio.AgregarAsync(pago);

        return Result<PagoDTO>.Ok(MapToDTO(pago), "Pago registrado correctamente.");
    }

    public async Task<Result<PagoDTO>> ObtenerPagoPorOrdenAsync(int ordenId)
    {
        if (ordenId <= 0)
            return Result<PagoDTO>.Fail("OrdenId inválido.");

        var pago = await _pagoRepositorio.ObtenerPorOrdenAsync(ordenId);

        if (pago is null)
            return Result<PagoDTO>.Fail("Pago no encontrado.");

        return Result<PagoDTO>.Ok(MapToDTO(pago));
    }

    private static PagoDTO MapToDTO(Pago pago)
    {
        return new PagoDTO
        {
            Id = pago.Id,
            Monto = pago.Monto,
            MetodoPago = pago.MetodoPago,
            EstadoPago = pago.EstadoPago,
            Fecha = pago.Fecha,
            OrdenId = pago.OrdenId
        };
    }
}