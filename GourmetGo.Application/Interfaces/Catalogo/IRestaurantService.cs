using GourmetGo.Application.Base;
using GourmetGo.Application.DTOs.Catalogo;
using GourmetGo.Application.DTOs.Catalogo.Restaurante;

namespace GourmetGo.Application.Interfaces.Catalogo;

public interface IRestauranteService
{
    Task<Result<List<RestauranteDTO>>> ObtenerTodosAsync();

    Task<Result<RestauranteDTO>> ObtenerPorIdAsync(int id);

    Task<Result<RestauranteDTO>> ObtenerPorUsuarioIdAsync(int usuarioId);

    Task<Result<string>> CrearAsync(CreateRestauranteDTO dto);
}