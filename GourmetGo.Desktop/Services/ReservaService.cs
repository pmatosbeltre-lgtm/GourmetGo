using GourmetGo.Desktop.Helpers;
using GourmetGo.Application.DTOs.Operaciones;
using GourmetGo.Application.Base;


namespace GourmetGo.Desktop.Services;

public class ReservaService
{
    private readonly ApiClient _api;

    public ReservaService(ApiClient api) => _api = api;

    public Task<Result<ReservaDTO>> ObtenerPorIdAsync(int id)
        => _api.GetAsync<Result<ReservaDTO>>($"/api/Reserva/{id}");

    public Task<Result<List<ReservaDTO>>> ObtenerPorRestauranteAsync(int restauranteId)
        => _api.GetAsync<Result<List<ReservaDTO>>>($"/api/Reserva/restaurante/{restauranteId}");

    public Task<Result<ReservaDTO>> CrearAsync(CreateReservaDTO dto)
        => _api.PostAsync<CreateReservaDTO, Result<ReservaDTO>>("/api/Reserva", dto);
}