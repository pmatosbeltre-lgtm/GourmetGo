using GourmetGo.Desktop.Helpers;
using GourmetGo.Application.DTOs.Catalogo;  
using GourmetGo.Application.DTOs.Catalogo.Menu;

namespace GourmetGo.Desktop.Services;

public class MenuService
{
    private readonly ApiClient _api;

    public MenuService(ApiClient api)
    {
        _api = api;
    }

    public Task<Result<List<MenuDTO>>> ObtenerPorRestauranteAsync(int restauranteId)
        => _api.GetAsync<Result<List<MenuDTO>>>($"/api/Menu/restaurante/{restauranteId}");

    public Task<Result<MenuDTO>> CrearAsync(CreateMenuDTO dto)
        => _api.PostAsync<CreateMenuDTO, Result<MenuDTO>>("/api/Menu", dto);
}