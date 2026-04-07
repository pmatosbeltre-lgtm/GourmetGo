using GourmetGo.Application.DTOs.Operaciones;
using GourmetGo.Application.Base; 

namespace GourmetGo.Application.Interfaces.Operaciones;

public interface IReservaService
{
        Task<Result<ReservaDTO>> CrearReservaAsync(CreateReservaDTO dto);

    Task<Result<ReservaDTO>> ObtenerReservaPorIdAsync(int id);

    Task<Result<IEnumerable<ReservaDTO>>> ObtenerReservasPorRestauranteAsync(int restauranteId);
}