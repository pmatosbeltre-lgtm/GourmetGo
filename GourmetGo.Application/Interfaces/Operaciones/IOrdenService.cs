using GourmetGo.Application.Base;
using GourmetGo.Application.DTOs.Operaciones;

namespace GourmetGo.Application.Interfaces.Operaciones;

public interface IOrdenService
{
    Task<Result<OrdenDTO>> CrearOrdenAsync(CreateOrdenDTO dto);

    Task<Result<OrdenDTO>> ObtenerOrdenPorIdAsync(int id);

    Task<Result<List<OrdenDTO>>> ObtenerOrdenesPorUsuarioAsync(int usuarioId);

    Task<Result<string>> ActualizarEstadoAsync(int id, UpdateOrdenDTO dto);

    Task<Result<List<OrdenDTO>>> ObtenerOrdenesPorRestauranteAsync(int restauranteId);

}