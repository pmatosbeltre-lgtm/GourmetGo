using GourmetGo.Application.Base;
using GourmetGo.Application.DTOs.Social;

namespace GourmetGo.Application.Interfaces.Social;

public interface IResenaService
{
    Task<Result<ResenaDTO>> CrearResenaAsync(CreateResenaDTO dto);

    Task<Result<List<ResenaDTO>>> ObtenerResenasPorRestauranteAsync(int restauranteId);
}