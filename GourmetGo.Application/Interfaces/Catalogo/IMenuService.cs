using GourmetGo.Application.Base;
using GourmetGo.Application.DTOs.Catalogo;
using GourmetGo.Application.DTOs.Catalogo.Menu;

namespace GourmetGo.Application.Interfaces.Catalogo
{
    public interface IMenuService
    {
        Task<Result<List<MenuDTO>>> ObtenerPorRestauranteAsync(int restauranteId);

        Task<Result<string>> CrearAsync(CreateMenuDTO dto);
    }
}