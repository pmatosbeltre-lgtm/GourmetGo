using GourmetGo.Desktop.Services;
using GourmetGo.Application.DTOs.Catalogo;
using GourmetGo.Application.DTOs.Catalogo.Menu;
using GourmetGo.Application.Base;

namespace GourmetGo.Desktop.ViewModels;

public class MenusViewModel
{
    private readonly MenuService _menuService;

    public int RestauranteId { get; set; }
    public List<MenuDTO> Menus { get; private set; } = new();

    public MenusViewModel(MenuService menuService)
    {
        _menuService = menuService;
    }

    public async Task<Result<List<MenuDTO>>> CargarPorRestauranteAsync()
    {
        var result = await _menuService.ObtenerPorRestauranteAsync(RestauranteId);
        if (result.Success && result.Data is not null)
            Menus = result.Data;
        return result;
    }

    public Task<Result<MenuDTO>> CrearAsync(CreateMenuDTO dto)
        => _menuService.CrearAsync(dto);
}