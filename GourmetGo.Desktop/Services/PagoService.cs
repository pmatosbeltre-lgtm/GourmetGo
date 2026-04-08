using GourmetGo.Desktop.Helpers;
using GourmetGo.Application.DTOs.Operaciones;
using GourmetGo.Application.Base;


namespace GourmetGo.Desktop.Services;

public class PagoService
{
    private readonly ApiClient _api;

    public PagoService(ApiClient api) => _api = api;

    public Task<Result<PagoDTO>> ObtenerPorOrdenAsync(int ordenId)
        => _api.GetAsync<Result<PagoDTO>>($"/api/Pago/orden/{ordenId}");

    public Task<Result<PagoDTO>> RegistrarAsync(CreatePagoDTO dto)
        => _api.PostAsync<CreatePagoDTO, Result<PagoDTO>>("/api/Pago", dto);
}