using GourmetGo.Desktop.Helpers;
using GourmetGo.Desktop.Services;
using GourmetGo.Application.DTOs.Operaciones;

namespace GourmetGo.Desktop.ViewModels;

public class PagosViewModel
{
    private readonly PagoService _pagoService;

    public int OrdenId { get; set; }
    public PagoDTO? Pago { get; private set; }

    public PagosViewModel(PagoService pagoService)
    {
        _pagoService = pagoService;
    }

    public async Task<Result<PagoDTO>> CargarPorOrdenAsync()
    {
        var result = await _pagoService.ObtenerPorOrdenAsync(OrdenId);
        if (result.Success)
            Pago = result.Data;
        return result;
    }

    public async Task<Result<PagoDTO>> RegistrarAsync(CreatePagoDTO dto)
    {
        var result = await _pagoService.RegistrarAsync(dto);
        if (result.Success)
            Pago = result.Data;
        return result;
    }
}