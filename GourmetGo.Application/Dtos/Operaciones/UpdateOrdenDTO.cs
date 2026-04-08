using GourmetGo.Domain.Enums;

namespace GourmetGo.Application.DTOs.Operaciones;

public class UpdateOrdenDTO
{
    public EstadoOrden NuevoEstado { get; set; }
}