using GourmetGo.Application.Base;
using GourmetGo.Application.DTOs.Operaciones;

namespace GourmetGo.Application.Interfaces.Operaciones;

public interface IDetalleOrdenService
{
    Task<Result<DetalleOrdenDTO>> CrearAsync(CreateDetalleOrdenDTO dto);

    Task<Result<List<DetalleOrdenDTO>>> ObtenerPorOrdenAsync(int ordenId);
    
}