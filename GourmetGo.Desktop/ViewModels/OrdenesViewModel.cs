using GourmetGo.Desktop.Helpers;
using GourmetGo.Desktop.Services;
using GourmetGo.Application.DTOs.Operaciones;

namespace GourmetGo.Desktop.ViewModels;

public class OrdenesViewModel
{
    private readonly OrdenService _ordenService;

    public int UsuarioId { get; set; }
    public List<OrdenDTO> Ordenes { get; private set; } = new();

    public OrdenesViewModel(OrdenService ordenService)
    {
        _ordenService = ordenService;
    }

    public async Task<Result<List<OrdenDTO>>> CargarPorUsuarioAsync()
    {
        var result = await _ordenService.ObtenerPorUsuarioAsync(UsuarioId);
        if (result.Success && result.Data is not null)
            Ordenes = result.Data;
        return result;
    }

    public Task<Result<OrdenDTO>> CrearAsync(CreateOrdenDTO dto)
        => _ordenService.CrearAsync(dto);

    public Task<Result<OrdenDTO>> ObtenerPorIdAsync(int id)
        => _ordenService.ObtenerPorIdAsync(id);
}