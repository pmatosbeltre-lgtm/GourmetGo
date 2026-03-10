using GourmetGo.Application.DTOs.Social;

namespace GourmetGo.Application.Interfaces.Social;

public interface IResenaService
{
    Task<ResenaDTO> CrearResenaAsync(CreateResenaDTO dto);

    Task<IEnumerable<ResenaDTO>> ObtenerResenasPorRestauranteAsync(int restauranteId);
}