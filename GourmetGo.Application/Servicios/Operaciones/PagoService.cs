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
        _pagoRepositorio = pagoRepositorio
            ?? throw new ArgumentNullException(nameof(pagoRepositorio));
    }

    public async Task<PagoDTO> RegistrarPagoAsync(CreatePagoDTO dto)
    {
        if (dto == null)
            throw new ArgumentNullException(nameof(dto));

        if (dto.OrdenId <= 0)
            throw new ArgumentException("Orden inválida.");

        if (dto.Monto <= 0)
            throw new ArgumentException("Monto inválido.");

        if (string.IsNullOrWhiteSpace(dto.MetodoPago))
            throw new ArgumentException("Método de pago inválido.");

        var pago = new Pago(
            dto.Monto,
            dto.MetodoPago,
            dto.OrdenId
        );

        await _pagoRepositorio.AgregarAsync(pago);

        return MapToDTO(pago);
    }

    public async Task<PagoDTO?> ObtenerPagoPorOrdenAsync(int ordenId)
    {
        if (ordenId <= 0)
            throw new ArgumentException("Orden inválida.");

        var pago = await _pagoRepositorio.ObtenerPorOrdenAsync(ordenId);

        if (pago == null)
            return null;

        return MapToDTO(pago);
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
