using GourmetGo.Application.DTOs.Operaciones;

namespace GourmetGo.Application.Interfaces.Operaciones;

public interface IPagoService
{
    Task<PagoDTO> RegistrarPagoAsync(CreatePagoDTO dto);

    Task<PagoDTO?> ObtenerPagoPorOrdenAsync(int ordenId);
}