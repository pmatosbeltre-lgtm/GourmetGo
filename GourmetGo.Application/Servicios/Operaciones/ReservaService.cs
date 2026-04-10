using GourmetGo.Application.DTOs.Operaciones;
using GourmetGo.Application.Interfaces.Operaciones;
using GourmetGo.Application.Base; 
using GourmetGo.Domain.Entidades;
using GourmetGo.Domain.Interfaces;

namespace GourmetGo.Application.Services.Operaciones;

public class ReservaService : IReservaService
{
    private readonly IReservaRepositorio _reservaRepositorio;

    public ReservaService(IReservaRepositorio reservaRepositorio)
    {
        _reservaRepositorio = reservaRepositorio
            ?? throw new ArgumentNullException(nameof(reservaRepositorio));
    }

    public async Task<Result<ReservaDTO>> CrearReservaAsync(CreateReservaDTO dto)
    {
        
        if (dto == null)
            return Result<ReservaDTO>.Fail("La información de la reserva no puede estar vacía.");

        if (dto.UsuarioId <= 0)
            return Result<ReservaDTO>.Fail("Usuario inválido.");

        if (dto.RestauranteId <= 0)
            return Result<ReservaDTO>.Fail("Restaurante inválido.");

        if (dto.CantidadPersonas <= 0)
            return Result<ReservaDTO>.Fail("Cantidad de personas inválida.");

        var reserva = new Reserva(
            dto.Fecha,
            dto.Hora,
            dto.CantidadPersonas,
            dto.UsuarioId,
            dto.RestauranteId
        );

        await _reservaRepositorio.AgregarAsync(reserva);

        
        return Result<ReservaDTO>.Ok(MapToDTO(reserva), "Reserva creada exitosamente.");
    }
    public async Task<Result<ReservaDTO>> ObtenerReservaPorIdAsync(int id)
    {
        if (id <= 0)
            return Result<ReservaDTO>.Fail("El id debe ser válido.");

        var reserva = await _reservaRepositorio.ObtenerPorIdAsync(id);

        if (reserva == null)
            return Result<ReservaDTO>.Fail("No se encontró la reserva solicitada."); 

        return Result<ReservaDTO>.Ok(MapToDTO(reserva));
    }

    public async Task<Result<IEnumerable<ReservaDTO>>> ObtenerReservasPorRestauranteAsync(int restauranteId)
    {
        if (restauranteId <= 0)
            return Result<IEnumerable<ReservaDTO>>.Fail("El id del restaurante es inválido.");

        var reservas = await _reservaRepositorio.ObtenerPorRestauranteAsync(restauranteId);

        
        return Result<IEnumerable<ReservaDTO>>.Ok(reservas.Select(MapToDTO));
    }

    private static ReservaDTO MapToDTO(Reserva reserva)
    {
        return new ReservaDTO
        {
            Id = reserva.Id,
            Fecha = reserva.Fecha,
            Hora = reserva.Hora,
            CantidadPersonas = reserva.CantidadPersonas,
            Estado = reserva.Estado,
            UsuarioId = reserva.UsuarioId,
            RestauranteId = reserva.RestauranteId
        };
    }
    public async Task<Result<List<ReservaDTO>>> ObtenerReservasPorUsuarioAsync(int usuarioId)
    {
        if (usuarioId <= 0)
            return Result<List<ReservaDTO>>.Fail("El id del usuario es inválido.");

        
        var reservas = await _reservaRepositorio.ObtenerPorUsuarioAsync(usuarioId);

        
        return Result<List<ReservaDTO>>.Ok(reservas.Select(MapToDTO).ToList());
    }

}