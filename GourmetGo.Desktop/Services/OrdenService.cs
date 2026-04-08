using GourmetGo.Desktop.Helpers;
using GourmetGo.Application.DTOs.Operaciones;
using GourmetGo.Application.Base;


namespace GourmetGo.Desktop.Services;

public class OrdenService
{
    private readonly ApiClient _api;

    public OrdenService(ApiClient api) => _api = api;

    public Task<Result<OrdenDTO>> ObtenerPorIdAsync(int id)
        => _api.GetAsync<Result<OrdenDTO>>($"/api/Orden/{id}");

    public Task<Result<List<OrdenDTO>>> ObtenerPorUsuarioAsync(int usuarioId)
        => _api.GetAsync<Result<List<OrdenDTO>>>($"/api/Orden/usuario/{usuarioId}");

    public Task<Result<OrdenDTO>> CrearAsync(CreateOrdenDTO dto)
        => _api.PostAsync<CreateOrdenDTO, Result<OrdenDTO>>("/api/Orden", dto);
}