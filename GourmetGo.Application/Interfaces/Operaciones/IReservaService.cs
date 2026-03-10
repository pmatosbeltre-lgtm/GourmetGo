using GourmetGo.Application.DTOs.Operaciones;

namespace GourmetGo.Application.Interfaces.Operaciones;

public interface IReservaService
{
    Task<ReservaDTO> CrearReservaAsync(CreateReservaDTO dto);

    Task<ReservaDTO?> ObtenerReservaPorIdAsync(int id);

    Task<IEnumerable<ReservaDTO>> ObtenerReservasPorRestauranteAsync(int restauranteId);
}