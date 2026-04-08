using GourmetGo.Application.Base;
using GourmetGo.Desktop.Services;
using GourmetGo.Application.DTOs.Operaciones;

namespace GourmetGo.Desktop.ViewModels;

public class ReservasViewModel
{
    private readonly ReservaService _reservaService;

    public int RestauranteId { get; set; }
    public List<ReservaDTO> Reservas { get; private set; } = new();

    public ReservasViewModel(ReservaService reservaService)
    {
        _reservaService = reservaService;
    }

    public async Task<Result<List<ReservaDTO>>> CargarPorRestauranteAsync()
    {
        var result = await _reservaService.ObtenerPorRestauranteAsync(RestauranteId);
        if (result.Success && result.Data is not null)
            Reservas = result.Data;
        return result;
    }

    public Task<Result<ReservaDTO>> CrearAsync(CreateReservaDTO dto)
        => _reservaService.CrearAsync(dto);

    public Task<Result<ReservaDTO>> ObtenerPorIdAsync(int id)
        => _reservaService.ObtenerPorIdAsync(id);
}