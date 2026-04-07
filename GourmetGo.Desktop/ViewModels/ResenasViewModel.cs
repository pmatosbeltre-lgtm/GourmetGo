using GourmetGo.Desktop.Helpers;
using GourmetGo.Desktop.Services;
using GourmetGo.Application.DTOs.Social;

namespace GourmetGo.Desktop.ViewModels;

public class ResenasViewModel
{
    private readonly ResenaService _resenaService;

    public int RestauranteId { get; set; }
    public List<ResenaDTO> Resenas { get; private set; } = new();

    public ResenasViewModel(ResenaService resenaService)
    {
        _resenaService = resenaService;
    }

    public async Task<Result<List<ResenaDTO>>> CargarPorRestauranteAsync()
    {
        var result = await _resenaService.ObtenerPorRestauranteAsync(RestauranteId);
        if (result.Success && result.Data is not null)
            Resenas = result.Data;
        return result;
    }

    public Task<Result<ResenaDTO>> CrearAsync(CreateResenaDTO dto)
        => _resenaService.CrearAsync(dto);
}