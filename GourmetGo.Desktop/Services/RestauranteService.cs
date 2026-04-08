using GourmetGo.Application.Base;
using GourmetGo.Application.DTOs.Catalogo;
using GourmetGo.Desktop.Helpers;

namespace GourmetGo.Desktop.Services;

public class RestauranteService
{
    private readonly ApiClient _api;

    public RestauranteService(ApiClient api)
    {
        _api = api;
    }

    public Task<Result<RestauranteDTO>> ObtenerMioAsync(int usuarioId)
        => _api.GetAsync<Result<RestauranteDTO>>($"/api/Restaurante/mio/{usuarioId}");
}