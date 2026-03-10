using GourmetGo.Application.DTOs.Operaciones;
using GourmetGo.Application.Interfaces.Operaciones;
using GourmetGo.Domain.Entidades;
using GourmetGo.Domain.Interfaces;

namespace GourmetGo.Application.Services.Operaciones;

public class OrdenService : IOrdenService
{
    private readonly IOrdenRepositorio _ordenRepositorio;

    public OrdenService(IOrdenRepositorio ordenRepositorio)
    {
        _ordenRepositorio = ordenRepositorio
            ?? throw new ArgumentNullException(nameof(ordenRepositorio));
    }

    public async Task<OrdenDTO> CrearOrdenAsync(CreateOrdenDTO dto)
    {
        if (dto == null)
            throw new ArgumentNullException(nameof(dto));

        var orden = new Orden(
            dto.Fecha,
            dto.TipoOrden,
            dto.UsuarioId,
            dto.RestauranteId
        );

        await _ordenRepositorio.AgregarAsync(orden);

        return MapToDTO(orden);
    }

    public async Task<OrdenDTO?> ObtenerOrdenPorIdAsync(int id)
    {
        var orden = await _ordenRepositorio.ObtenerPorIdAsync(id);

        if (orden == null)
            return null;

        return MapToDTO(orden);
    }

    public async Task<IEnumerable<OrdenDTO>> ObtenerOrdenesPorUsuarioAsync(int usuarioId)
    {
        var ordenes = await _ordenRepositorio.ObtenerPorUsuarioAsync(usuarioId);

        return ordenes.Select(MapToDTO);
    }

    private static OrdenDTO MapToDTO(Orden orden)
    {
        return new OrdenDTO
        {
            Id = orden.Id,
            Fecha = orden.Fecha,
            Total = orden.Total,
            Estado = orden.Estado,
            TipoOrden = orden.TipoOrden,
            UsuarioId = orden.UsuarioId,
            RestauranteId = orden.RestauranteId
        };
    }
}