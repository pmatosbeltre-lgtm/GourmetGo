using GourmetGo.Desktop.Helpers;
using GourmetGo.Application.DTOs.Social;

namespace GourmetGo.Desktop.Services;

public class ResenaService
{
    private readonly ApiClient _api;

    public ResenaService(ApiClient api)
    {
        _api = api;
    }

    public Task<Result<List<ResenaDTO>>> ObtenerPorRestauranteAsync(int restauranteId)
        => _api.GetAsync<Result<List<ResenaDTO>>>($"/api/Resena/restaurante/{restauranteId}");

    public Task<Result<ResenaDTO>> CrearAsync(CreateResenaDTO dto)
        => _api.PostAsync<CreateResenaDTO, Result<ResenaDTO>>("/api/Resena", dto);
}