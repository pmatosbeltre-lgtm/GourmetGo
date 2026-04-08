using GourmetGo.Application.Base;
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
        _ordenRepositorio = ordenRepositorio;
    }
    public async Task<Result<string>> ActualizarEstadoAsync(int id, UpdateOrdenDTO dto)
    {
        if (id <= 0)
            return Result<string>.Fail("El ID de la orden no es válido.");

        if (dto is null)
            return Result<string>.Fail("El DTO no puede estar vacío.");

        var orden = await _ordenRepositorio.ObtenerPorIdAsync(id);

        if (orden is null)
            return Result<string>.Fail("Orden no encontrada.");

        try
        {
            orden.CambiarEstado(dto.NuevoEstado);
            await _ordenRepositorio.ActualizarAsync(orden);
            return Result<string>.Ok("Estado actualizado correctamente.");
        }
        catch (Exception ex)
        {
            return Result<string>.Fail(ex.Message);
        }
    }
    public async Task<Result<OrdenDTO>> CrearOrdenAsync(CreateOrdenDTO dto)
    {
        // Validaciones (estructurales / de entrada)
        if (dto is null)
            return Result<OrdenDTO>.Fail("La orden no puede estar vacía.");

        if (dto.UsuarioId <= 0)
            return Result<OrdenDTO>.Fail("UsuarioId inválido.");

        if (dto.RestauranteId <= 0)
            return Result<OrdenDTO>.Fail("RestauranteId inválido.");

        if (string.IsNullOrWhiteSpace(dto.TipoOrden))
            return Result<OrdenDTO>.Fail("TipoOrden es obligatorio.");


        var orden = new Orden(
            dto.Fecha,
            dto.TipoOrden,
            dto.UsuarioId,
            dto.RestauranteId
        );

        await _ordenRepositorio.AgregarAsync(orden);

        return Result<OrdenDTO>.Ok(MapToDTO(orden), "Orden creada correctamente.");
    }

    public async Task<Result<OrdenDTO>> ObtenerOrdenPorIdAsync(int id)
    {
        if (id <= 0)
            return Result<OrdenDTO>.Fail("El ID de la orden no es válido.");

        var orden = await _ordenRepositorio.ObtenerPorIdAsync(id);

        if (orden is null)
            return Result<OrdenDTO>.Fail("Orden no encontrada.");

        return Result<OrdenDTO>.Ok(MapToDTO(orden));
    }

    public async Task<Result<List<OrdenDTO>>> ObtenerOrdenesPorUsuarioAsync(int usuarioId)
    {
        if (usuarioId <= 0)
            return Result<List<OrdenDTO>>.Fail("El ID del usuario no es válido.");

        var ordenes = await _ordenRepositorio.ObtenerPorUsuarioAsync(usuarioId);

        var data = ordenes.Select(MapToDTO).ToList();

        return Result<List<OrdenDTO>>.Ok(data);
    }
    public async Task<Result<List<OrdenDTO>>> ObtenerOrdenesPorRestauranteAsync(int restauranteId)
    {
        if (restauranteId <= 0)
            return Result<List<OrdenDTO>>.Fail("El ID del restaurante no es válido.");

        try
        {
            var ordenes = await _ordenRepositorio.ObtenerPorRestauranteAsync(restauranteId);
            var data = ordenes.Select(MapToDTO).ToList();
            return Result<List<OrdenDTO>>.Ok(data);
        }
        catch (Exception ex)
        {
            return Result<List<OrdenDTO>>.Fail($"Error obteniendo órdenes: {ex.Message}");
        }
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