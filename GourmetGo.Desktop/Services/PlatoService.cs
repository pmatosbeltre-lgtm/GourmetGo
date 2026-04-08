using GourmetGo.Desktop.Helpers;
using GourmetGo.Application.DTOs.Catalogo;
using GourmetGo.Application.DTOs.Catalogo.Plato;
using GourmetGo.Application.Base;



namespace GourmetGo.Desktop.Services;

public class PlatoService
{
    private readonly ApiClient _api;

    public PlatoService(ApiClient api) => _api = api;

    public Task<Result<List<PlatoDTO>>> ObtenerPorMenuAsync(int menuId)
        => _api.GetAsync<Result<List<PlatoDTO>>>($"/api/Plato/menu/{menuId}");

    public Task<Result<string>> CrearAsync(CreatePlatoDTO dto)
        => _api.PostAsync<CreatePlatoDTO, Result<string>>("/api/Plato", dto);
}