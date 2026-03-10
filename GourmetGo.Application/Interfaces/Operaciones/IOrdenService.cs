using GourmetGo.Application.DTOs.Operaciones;

namespace GourmetGo.Application.Interfaces.Operaciones;

public interface IOrdenService
{
    Task<OrdenDTO> CrearOrdenAsync(CreateOrdenDTO dto);

    Task<OrdenDTO?> ObtenerOrdenPorIdAsync(int id);

    Task<IEnumerable<OrdenDTO>> ObtenerOrdenesPorUsuarioAsync(int usuarioId);
}