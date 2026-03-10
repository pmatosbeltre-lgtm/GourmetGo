using GourmetGo.Application.DTOs.Operaciones;
using GourmetGo.Application.Interfaces.Operaciones;
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

    public async Task<ReservaDTO> CrearReservaAsync(CreateReservaDTO dto)
    {
        if (dto == null)
            throw new ArgumentNullException(nameof(dto));

        if (dto.UsuarioId <= 0)
            throw new ArgumentException("Usuario inválido.");

        if (dto.RestauranteId <= 0)
            throw new ArgumentException("Restaurante inválido.");

        if (dto.CantidadPersonas <= 0)
            throw new ArgumentException("Cantidad de personas inválida.");

        var reserva = new Reserva(
            dto.Fecha,
            dto.Hora,
            dto.CantidadPersonas,
            dto.UsuarioId,
            dto.RestauranteId
        );

        await _reservaRepositorio.AgregarAsync(reserva);

        return MapToDTO(reserva);
    }

    public async Task<ReservaDTO?> ObtenerReservaPorIdAsync(int id)
    {
        if (id <= 0)
            throw new ArgumentException("El id debe ser válido.");

        var reserva = await _reservaRepositorio.ObtenerPorIdAsync(id);

        if (reserva == null)
            return null;

        return MapToDTO(reserva);
    }

    public async Task<IEnumerable<ReservaDTO>> ObtenerReservasPorRestauranteAsync(int restauranteId)
    {
        if (restauranteId <= 0)
            throw new ArgumentException("El id del restaurante es inválido.");

        var reservas = await _reservaRepositorio.ObtenerPorRestauranteAsync(restauranteId);

        return reservas.Select(MapToDTO);
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
}