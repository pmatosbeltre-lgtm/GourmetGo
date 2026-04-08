using GourmetGo.Desktop.Helpers;
using GourmetGo.Application.DTOs.Catalogo;  
using GourmetGo.Application.DTOs.Catalogo.Menu;
using GourmetGo.Application.Base;

namespace GourmetGo.Desktop.Services;

public class MenuService
{
    private readonly ApiClient _api;

    public MenuService(ApiClient api) => _api = api;

    public Task<Result<List<MenuDTO>>> ObtenerPorRestauranteAsync(int restauranteId)
        => _api.GetAsync<Result<List<MenuDTO>>>($"/api/Menu/restaurante/{restauranteId}");

    public Task<Result<MenuDTO>> ObtenerPorIdAsync(int id)
        => _api.GetAsync<Result<MenuDTO>>($"/api/Menu/{id}");

    public async Task<Result<MenuDTO>> CrearAsync(CreateMenuDTO dto)
    {
        return await _api.PostAsync<CreateMenuDTO, Result<MenuDTO>>("api/Menu", dto);
    }

    public Task<Result<string>> ActualizarAsync(int id, UpdateMenuDTO dto)
        => _api.PutAsync<UpdateMenuDTO, Result<string>>($"/api/Menu/{id}", dto);

    public Task<Result<string>> EliminarAsync(int id)
        => _api.DeleteAsync<Result<string>>($"/api/Menu/{id}");
}