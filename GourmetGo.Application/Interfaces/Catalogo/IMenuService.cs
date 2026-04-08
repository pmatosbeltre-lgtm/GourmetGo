using GourmetGo.Application.Base;
using GourmetGo.Application.DTOs.Catalogo;
using GourmetGo.Application.DTOs.Catalogo.Menu;

namespace GourmetGo.Application.Interfaces.Catalogo
{
    public interface IMenuService
    {
        Task<Result<List<MenuDTO>>> ObtenerPorRestauranteAsync(int restauranteId);
        Task<Result<string>> CrearAsync(CreateMenuDTO dto);
        Task<Result<MenuDTO>> ObtenerPorIdAsync(int id);
        Task<Result<string>> ActualizarAsync(int id, UpdateMenuDTO dto);
        Task<Result<string>> EliminarAsync(int id);
    }
}