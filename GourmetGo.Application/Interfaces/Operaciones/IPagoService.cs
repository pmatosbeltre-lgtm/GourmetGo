using GourmetGo.Application.Base;
using GourmetGo.Application.DTOs.Operaciones;

namespace GourmetGo.Application.Interfaces.Operaciones;

public interface IPagoService
{
    Task<Result<PagoDTO>> RegistrarPagoAsync(CreatePagoDTO dto);

    Task<Result<PagoDTO>> ObtenerPagoPorOrdenAsync(int ordenId);
}