using GourmetGo.Application.Base;
using GourmetGo.Application.DTOs.Operaciones;
using GourmetGo.Application.Interfaces.Operaciones;
using GourmetGo.Domain.Entidades;
using GourmetGo.Domain.Interfaces;

namespace GourmetGo.Application.Services.Operaciones;

public class DetalleOrdenService : IDetalleOrdenService
{
    private readonly IDetalleOrdenRepositorio _detalleOrdenRepositorio;

    public DetalleOrdenService(IDetalleOrdenRepositorio detalleOrdenRepositorio)
    {
        _detalleOrdenRepositorio = detalleOrdenRepositorio;
    }

    public async Task<Result<DetalleOrdenDTO>> CrearAsync(CreateDetalleOrdenDTO dto)
    {
        if (dto is null)
            return Result<DetalleOrdenDTO>.Fail("El detalle de orden no puede estar vacío.");

        // Validación de borde/estructura 
        if (dto.OrdenId <= 0)
            return Result<DetalleOrdenDTO>.Fail("OrdenId inválido.");

        if (dto.PlatoId <= 0)
            return Result<DetalleOrdenDTO>.Fail("PlatoId inválido.");

        if (dto.Cantidad <= 0)
            return Result<DetalleOrdenDTO>.Fail("Cantidad inválida.");

        if (dto.PrecioUnitario < 0)
            return Result<DetalleOrdenDTO>.Fail("PrecioUnitario inválido.");

        var entity = new DetalleOrden(dto.OrdenId, dto.PlatoId, dto.Cantidad, dto.PrecioUnitario);

        await _detalleOrdenRepositorio.AgregarAsync(entity);

        var response = new DetalleOrdenDTO
        {
            Id = entity.Id,
            OrdenId = entity.OrdenId,
            PlatoId = entity.PlatoId,
            Cantidad = entity.Cantidad,
            PrecioUnitario = entity.PrecioUnitario
        };

        return Result<DetalleOrdenDTO>.Ok(response, "Detalle de orden creado correctamente.");
    }

    public async Task<Result<List<DetalleOrdenDTO>>> ObtenerPorOrdenAsync(int ordenId)
    {
        if (ordenId <= 0)
            return Result<List<DetalleOrdenDTO>>.Fail("OrdenId inválido.");

        var detalles = await _detalleOrdenRepositorio.ObtenerPorOrdenAsync(ordenId);

        var data = detalles.Select(d => new DetalleOrdenDTO
        {
            Id = d.Id,
            OrdenId = d.OrdenId,
            PlatoId = d.PlatoId,
            Cantidad = d.Cantidad,
            PrecioUnitario = d.PrecioUnitario
        }).ToList();

        return Result<List<DetalleOrdenDTO>>.Ok(data);
    }
}